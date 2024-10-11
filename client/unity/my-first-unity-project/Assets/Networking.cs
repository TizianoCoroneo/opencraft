using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;
using Opencraft.NetCode;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System.Linq;
using System.Net;
using UnityEngine.Assertions;
using System;

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

    private UdpClient client;
    private ConcurrentQueue<UdpReceiveResult> messageQueue = default;
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
                var b = message.ToByteArray();
                Debug.Log($"tx {b.Length} byte msg {message}");
                client?.Send(b, b.Length);
            }
        }

        var nMessages = messageQueue.Count;
        for (var i = 0; i < nMessages; i++)
        {
            if (messageQueue.TryDequeue(out var message))
            {
                HandleMessage(message);
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
        outgoingMessages.Enqueue(loginMsg);
    }

    public void SendToServer(ToServer message)
    {
        outgoingMessages.Enqueue(message);
    }

    private void HandleMessage(UdpReceiveResult message)
    {
        var toClient = ToClient.Parser.ParseFrom(message.Buffer);
        Debug.Log($"rx {message.Buffer.Length} byte msg {toClient}");
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
        receiveLoop = Task.Run(async () =>
        {
            running = true;
            while (running)
            {
                try
                {
                    var msg = await client.ReceiveAsync();
                    messageQueue.Enqueue(msg);
                }
                catch (ObjectDisposedException e)
                {
                    if (running)
                    {
                        Debug.Log(e);
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
