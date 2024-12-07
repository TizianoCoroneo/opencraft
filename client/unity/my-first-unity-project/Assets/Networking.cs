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
using System.Diagnostics;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// This class takes care of the client-server networking. It sends packets to
/// the server, and it receives and handles packets received from the server.
/// </summary>
public class Networking : MonoBehaviour
{
    public GameObject playerCharacter;
    public World world;
    [SerializeField] private bool automaticLogin = default;
    [SerializeField] private GameManager gameManager = default;

    private TcpClient client;
    private ConcurrentQueue<(ToClient, TcpClient)> messageQueue = default;
    private ConcurrentQueue<ToServer> outgoingMessages = default;
    private bool running = true;
    private Task receiveLoop = default;
    private Stopwatch stopwatch = new();
    private float currentRTT = 35.0f;
    private bool isThinClient = false;
    [SerializeField] public bool isHomeSide = false;


    // Start is called before the first frame update
    void Start()
    {
        messageQueue = new();
        outgoingMessages = new();
        if (isHomeSide)
            stopwatch.Start();

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
        if (isHomeSide && stopwatch.ElapsedMilliseconds > 1000)
        {
            var ping = new ToServer
            {
                IWantOpenPing = new IWantOpenPing { TimeSent = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() }
            };
            SendToServer(ping);
            stopwatch.Restart();
        }

        var nOutgoing = outgoingMessages.Count;
        for (var i = 0; i < nOutgoing; i++)
        {
            if (outgoingMessages.TryDequeue(out var message))
            {
                // UnityEngine.Debug.Log($"Sending {message} to {client.Client.RemoteEndPoint}");
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
            case ToClient.PayloadOneofCase.OpenPing:
                HandleMessageOpenPing(toClient.OpenPing);
                break;
            default:
                UnityEngine.Debug.LogWarning($"Got unsupported message: {toClient.PayloadCase}");
                break;
        }
    }

    public void RequestColumn(Pos2 position)
    {
        // UnityEngine.Debug.Log($"Requesting column at {position}");

        var getColumn = new ToServer
        {
            IWantColumn = new IWantColumn
            {
                ColumnPos = position
            }
        };
        outgoingMessages.Enqueue(getColumn);
    }

    /// <summary>
    /// Handle the server's reply to our <see cref="LogIn"/> request. If the
    /// login was successful, we should receive a player ID.
    /// </summary>
    /// <param name="youArePlayer">The received reply.</param>
    private void HandleMessageLogin(YouArePlayer youArePlayer)
    {
        // UnityEngine.Debug.Log(youArePlayer.ToString());
        gameManager.PlayerID = youArePlayer.PlayerID;
        var pos = youArePlayer.SpawnLocation;
        playerCharacter.GetComponent<playerscript>().Teleport(new(pos.X, pos.Y, pos.Z));

        // Immediately ask for the column at 0,0
        RequestColumn(new Pos2 { X = 0, Z = 0 });
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
    /// Handle a server ping message. We use this to measure network latency.
    /// </summary>
    /// <param name="ping">The received ping message.</param>
    private void HandleMessageOpenPing(OpenPing ping)
    {
        currentRTT = currentRTT * 0.9f + ((ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds() - ping.TimeSent) * 0.1f;
        UnityEngine.Debug.Log($"Ping time taken: {(ulong)DateTimeOffset.Now.ToUnixTimeMilliseconds() - ping.TimeSent}ms");
        UnityEngine.Debug.Log($"Current RTT: {currentRTT}ms");

        if (isThinClient && currentRTT > 100)
        {
            StartCoroutine(BecomeClient());
        }
        else if (!isThinClient && currentRTT < 80)
        {
            StartCoroutine(BecomeThinClient());
        }
    }

    IEnumerator BecomeThinClient()
    {
        using var www = UnityWebRequest.Get("http://localhost:7980/become/thinclient?host=localhost&port=7999&signalingPort=7981");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) UnityEngine.Debug.Log(www.error);
        else
        {
            isThinClient = true;
            UnityEngine.Debug.Log("Became thin client!");
        }
    }

    IEnumerator BecomeClient()
    {
        using var www = UnityWebRequest.Get("http://localhost:7980/become/client?host=localhost&port=7979&playerID=1");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) UnityEngine.Debug.Log(www.error);
        else
        {
            isThinClient = false;
            UnityEngine.Debug.Log("Became client!");
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
                        UnityEngine.Debug.LogError(e);
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
