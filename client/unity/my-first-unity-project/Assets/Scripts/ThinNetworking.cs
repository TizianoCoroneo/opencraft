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
public class ThinNetworking : MonoBehaviour
{
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
    private Stopwatch stopwatch = new();
    private float currentRTT = 35.0f;
    private bool isThinClient = false;
    [SerializeField] public PolicyManager policyManager;


    // Start is called before the first frame update
    void Start()
    {
        messageQueue = new();
        outgoingMessages = new();
        
        ReInitSocket();
        
        stopwatch.Start();
    }

    /// <summary>
    /// Send all queued outgoing messages to the server, and process all queued
    /// incoming messages from the server.
    /// </summary>
    void Update()
    {
        if (stopwatch.ElapsedMilliseconds > 1000)
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
            case ToClient.PayloadOneofCase.OpenPing:
                HandleMessageOpenPing(toClient.OpenPing);
                break;
            default:
                UnityEngine.Debug.LogWarning($"Got unsupported message: {toClient.PayloadCase}");
                break;
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

        switch (policyManager.Policy.Evaluate(new Policy.PolicyData
                {
                    CurrentRTT = currentRTT
                }))
        {
            case Policy.PolicyResult.BecomeClient: StartCoroutine(BecomeClient()); break;
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
    
    public void ReInitSocket(string serverHost = "localhost", int port = 7979)
    {
        var addresses = Dns.GetHostAddresses(serverHost);
        Assert.IsTrue(addresses.Length > 0);
        var ip = addresses.Where(x => x.AddressFamily == AddressFamily.InterNetwork).First();
        ReInitSocket(new(ip, port));
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
