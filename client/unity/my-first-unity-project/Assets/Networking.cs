using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;
using Opencraft.NetCode;
using Google.Protobuf;
using System.Linq;
using System.Net;
using UnityEngine.Assertions;
using System;
using System.IO;

/// <summary>
/// This class takes care of the client-server networking. It sends packets to
/// the server, and it receives and handles packets received from the server.
/// </summary>
public class Networking : MonoBehaviour
{
    /// <summary>
    /// Reference to the player avatar object. Used to move the avatar to the
    /// correct location upon login. The field is set through the Unity editor.
    /// </summary>
    /// <seealso cref="HandleMessageLogin"/>
    public GameObject playerCharacter;

    /// <summary>
    /// Reference to the game world. Used to load new chunks received from the
    /// server. The field is set through the Unity editor.
    /// </summary>
    /// <seealso cref="HandleMessageColumnData"/>
    public World world;

    /// <summary>
    /// When set to true, tries to automatically log in to a server running on
    /// localhost, using the default port of 7979, when the game starts.
    ///
    /// <para><b>DEPRECATED</b> this field is deprecated. Set to false. Use the
    /// <see cref="Bootstrap"/> class to connect to a server upon boot.</para>
    /// </summary>
    [Obsolete("Logging in is now the responsibility of the Bootstrap class. Setting this value tries to log in on localhost.")]
    [SerializeField]
    private bool automaticLogin = default;

    /// <summary>
    /// Reference to the <see cref="GameManager"/> ScriptableObjects, which
    /// maintains information about the (ongoing) game. This class uses the game
    /// manager to save the player ID received from the server and the server
    /// address after successfully logging in.
    /// </summary>
    /// <seealso cref="HttpServer.HandleRequestBecomeThinClient"/>
    [SerializeField] private GameManager gameManager = default;

    /// <summary>
    /// The client used to communicate with the game server.
    /// </summary>
    private TcpClient client;

    /// <summary>
    /// A queue for incoming server messages, stored here when they are received
    /// but have not yet been processed.
    /// </summary>
    private ConcurrentQueue<(ToClient, TcpClient)> messageQueue = default;

    /// <summary>
    /// A queue for outgoing client messages, stored here when they have been
    /// generated but have not yet been sent using the <see cref="client"/>.
    /// </summary>
    private ConcurrentQueue<ToServer> outgoingMessages = default;

    /// <summary>
    /// Indicates whether there is an asynchronous task listening for incoming
    /// messages from the server.
    /// </summary>
    /// <seealso cref="messageQueue"/>
    /// <seealso cref="receiveLoop"/>
    /// <seealso cref="StartSocketReceive"/>
    private bool running = true;

    /// <summary>
    /// The task that reads incoming messages from <see cref="TcpClient"/> and
    /// enqueues them in the <see cref="messageQueue"/>.
    /// </summary>
    private Task receiveLoop = default;

    // Start is called before the first frame update
    void Start()
    {
        messageQueue = new();
        outgoingMessages = new();

        if (automaticLogin)
            // TODO support remote servers
            LogIn("localhost");
    }

    /// <summary>
    /// Send all queued outgoing messages to the server, and process all queued
    /// incoming messages from the server.
    /// </summary>
    void Update()
    {
        var nOutgoing = outgoingMessages.Count;
        for (var i = 0; i < nOutgoing; i++)
        {
            if (outgoingMessages.TryDequeue(out var message))
            {
                // Debug.Log($"Sending {message} to {client.Client.RemoteEndPoint}");
                message.WriteDelimitedTo(client.GetStream());
            }
        }

        var nMessages = messageQueue.Count;
        for (var i = 0; i < nMessages; i++)
        {
            if (messageQueue.TryDequeue(out var message))
            {
                HandleMessage(message.Item1, message.Item2);
            }
        }
    }

    /// <summary>
    /// Log in to a server.
    /// </summary>
    /// <param name="serverHost">Server address.</param>
    /// <param name="port">Server port.</param>
    /// <param name="playerID">Player ID. If 0, the server will assign us a
    /// player ID. If larger than 0, we request to log in as that player. A
    /// player can be logged in multiple times.</param>
    public void LogIn(string serverHost = "localhost", int port = 7979, int playerID = 0)
    {
        var addresses = Dns.GetHostAddresses(serverHost);
        Assert.IsTrue(addresses.Length > 0);
        var ip = addresses.Where(x => x.AddressFamily == AddressFamily.InterNetwork).First();
        LogIn(new(ip, port), playerID);
    }

    /// <summary>
    /// Log in to a server.
    /// </summary>
    /// <param name="server">The server endpoint (ip+port).</param>
    /// <param name="playerID">Player ID. If 0, the server will assign us a
    /// player ID. If larger than 0, we request to log in as that player. A
    /// player can be logged in multiple times.</param>
    public void LogIn(IPEndPoint server, int playerID = 0)
    {
        ReInitSocket(server);

        var loginMsg = new ToServer
        {
            IWantPlayer = new IWantPlayer { PlayerID = (uint)playerID }
        };
        SendToServer(loginMsg);
    }

    /// <summary>
    /// Enqueue a message to be send to the server. Will be sent when <see
    /// cref="Update"/> is called.
    /// </summary>
    /// <param name="message">The message to send.</param>
    public void SendToServer(ToServer message)
    {
        outgoingMessages.Enqueue(message);
    }

    /// <summary>
    /// Handle a single incoming message from the server.
    /// </summary>
    /// <param name="toClient">The received message.</param>
    /// <param name="client">The connection the message was received
    /// from.</param>
    private void HandleMessage(ToClient toClient, TcpClient client)
    {
        switch (toClient.PayloadCase)
        {
            case ToClient.PayloadOneofCase.YouArePlayer:
                HandleMessageLogin(toClient.YouArePlayer);
                break;
            case ToClient.PayloadOneofCase.ColumnData:
                HandleMessageColumnData(toClient.ColumnData);
                break;
            default:
                Debug.LogWarning($"Got unsupported message: {toClient.PayloadCase}");
                break;
        }
    }

    /// <summary>
    /// Handle the server's reply to our <see cref="LogIn"/> request. If the
    /// login was successful, we should receive a player ID.
    /// </summary>
    /// <param name="youArePlayer">The received reply.</param>
    private void HandleMessageLogin(YouArePlayer youArePlayer)
    {
        Debug.Log(youArePlayer.ToString());
        gameManager.PlayerID = youArePlayer.PlayerID;
        var pos = youArePlayer.SpawnLocation;
        playerCharacter.GetComponent<playerscript>().Teleport(new(pos.X, pos.Y, pos.Z));

        // Immediately ask for the column at 0,0
        var getColumn = new ToServer
        {
            IWantColumn = new IWantColumn
            {
                ColumnPos = new Pos2 { X = 0, Z = 0 }
            }
        };
        outgoingMessages.Enqueue(getColumn);
    }

    /// <summary>
    /// Handle a server message containing a column of blocks in the world. We
    /// should create that chunk and show it to the player.
    /// </summary>
    /// <param name="columnData">The received column of blocks.</param>
    private void HandleMessageColumnData(ColumnData columnData)
    {
        var pos = columnData.Position;
        var chunks = columnData.Chunks;
        foreach (var chunk in chunks)
        {
            var bytes = chunk.BlockTypes.ToByteArray();
            world.InstantiateChunk(new(pos.X, pos.Z), bytes);
        }
    }

    /// <summary>
    /// Close our current socket and create a new socket to start sending and
    /// receiving messages from the provided endpoint.
    /// </summary>
    /// <param name="server">The new endpoint to which to connect.</param>
    private void ReInitSocket(IPEndPoint server)
    {
        StopSocketReceive();
        StartSocketReceive(server);
    }

    /// <summary>
    /// Start a new thread that listens for incoming messages and enqueues them
    /// for later processing in our main game loop.
    /// </summary>
    /// <param name="server">The server to connect to.</param>
    private void StartSocketReceive(IPEndPoint server)
    {
        client = new();
        client.Connect(server);
        gameManager.ServerEndpoint = server;
        receiveLoop = Task.Run(() =>
        {
            running = true;
            var stream = client.GetStream();
            while (running)
            {
                try
                {
                    var msg = ToClient.Parser.ParseDelimitedFrom(stream);
                    messageQueue.Enqueue((msg, client));
                }
                catch (Exception e) when (e is ObjectDisposedException
                    || e is SocketException
                    || e is IOException)
                {
                    if (running)
                    {
                        Debug.LogError(e);
                    }
                }
            }
        });
    }

    /// <summary>
    /// Stop listening to and receiving packets from the server endpoint.
    /// </summary>
    private async void StopSocketReceive()
    {
        running = false;
        client?.Close();
        if (receiveLoop != null)
        {
            await receiveLoop;
        }
    }

    void OnDestroy()
    {
        StopSocketReceive();
    }
}
