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
    public GameObject playerCharacter;
    public World world;
    [SerializeField] private bool automaticLogin = default;
    [SerializeField] private GameManager gameManager = default;

    private TcpClient client;
    private ConcurrentQueue<(ToClient, TcpClient)> messageQueue = default;
    private ConcurrentQueue<ToServer> outgoingMessages = default;
    private bool running = true;
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

    // Update is called once per frame
    void Update()
    {
        var nOutgoing = outgoingMessages.Count;
        for (var i = 0; i < nOutgoing; i++)
        {
            if (outgoingMessages.TryDequeue(out var message))
            {
                Debug.Log($"Sending {message} to {client.Client.RemoteEndPoint}");
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

    public void LogIn(string serverHost = "localhost", int port = 7979, int playerID = 0)
    {
        var addresses = Dns.GetHostAddresses(serverHost);
        Assert.IsTrue(addresses.Length > 0);
        var ip = addresses.Where(x => x.AddressFamily == AddressFamily.InterNetwork).First();
        LogIn(new(ip, port), playerID);
    }

    public void LogIn(IPEndPoint server, int playerID = 0)
    {
        ReInitSocket(server);

        var loginMsg = new ToServer
        {
            IWantPlayer = new IWantPlayer { PlayerID = (uint)playerID }
        };
        SendToServer(loginMsg);
    }

    public void SendToServer(ToServer message)
    {
        outgoingMessages.Enqueue(message);
    }

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

    public void RequestColumn(Pos2 position) {
        Debug.Log($"Requesting column at {position}");
        
        var getColumn = new ToServer
        {
            IWantColumn = new IWantColumn
            {
                ColumnPos = position
            }
        };
        outgoingMessages.Enqueue(getColumn);
    }

    private void HandleMessageLogin(YouArePlayer youArePlayer)
    {
        Debug.Log(youArePlayer.ToString());
        gameManager.PlayerID = youArePlayer.PlayerID;
        var pos = youArePlayer.SpawnLocation;
        playerCharacter.GetComponent<playerscript>().Teleport(new(pos.X, pos.Y, pos.Z));

        // Immediately ask for the column at 0,0
        RequestColumn(new Pos2 { X = 0, Z = 0 });
    }

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

    private void ReInitSocket(IPEndPoint server)
    {
        StopSocketReceive();
        StartSocketReceive(server);
    }

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
