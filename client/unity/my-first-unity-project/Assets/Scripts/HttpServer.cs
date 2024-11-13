using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Unity.RenderStreaming;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;

public class HttpServer : MonoBehaviour
{
    public static HttpServer Instance { get; private set; }

    [SerializeField] private GameManager gameManager;
    [SerializeField] private int listenPort = 7980;

    private HttpListener httpListener = default;
    private bool listening = default;
    private Task listenLoop = default;
    private ConcurrentQueue<HttpListenerContext> requests = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        StartHttpServer();
    }

    void OnDisable()
    {
        StopHttpServer();
    }

    // Update is called once per frame
    void Update()
    {
        var nRequests = requests.Count();
        for (var i = 0; i < nRequests; i++)
        {
            var ok = requests.TryDequeue(out var req);
            Assert.IsTrue(ok);
            HandleRequest(req);
        }
    }

    private void StartHttpServer()
    {
        Debug.Log("Starting HTTP Server");
        if (httpListener != null)
        {
            Debug.LogWarning("there is already an http server!");
            return;
        }
        listening = true;
        httpListener = new();
        httpListener.Prefixes.Add($"http://*:{listenPort}/");
        foreach (var r in requests)
        {
            r.Response.Close();
        }
        requests.Clear();
        httpListener.Start();
        listenLoop = Task.Run(() => ListenForRequests());
    }

    private async void StopHttpServer()
    {
        Debug.Log("Stopping HTTP server");
        listening = false;
        httpListener?.Close();
        httpListener = null;
        if (listenLoop != null)
            await listenLoop;
    }


    private void ListenForRequests()
    {
        Debug.Log($"listening for http requests on {string.Join(',', httpListener.Prefixes)}");
        while (listening)
        {
            try
            {
                requests.Enqueue(httpListener.GetContext());
            }
            catch (HttpListenerException e)
            {
                if (listening)
                {
                    Debug.LogWarning(e);
                }
            }
        }
    }

    private void HandleRequest(HttpListenerContext context)
    {
        var req = context.Request;
        using var res = context.Response;

        Debug.Log($"received request: {req.Url}");

        // http://example.com/login/to/your/site gets parsed as
        // ["/","login/","to/","your/","site"] in req.Url.Segments
        var segments = req.Url.Segments
            .Select(x => x.Replace("/", ""))
            .Where(x => !string.IsNullOrEmpty(x))
            .ToArray();

        if (Enumerable.SequenceEqual(segments, new[] { "login" }))
        {
            HandleRequestLogin(req.QueryString);
        }
        else if (Enumerable.SequenceEqual(segments, new[] { "become", "thinclient" }))
        {
            HandleRequestBecomeThinClient(req.QueryString);
        }
        else if (Enumerable.SequenceEqual(segments, new[] { "become", "client" }))
        {
            HandleRequestBecomeClient(req.QueryString);
        }
        else
        {
            var x = string.Join("", req.Url.Segments);
            Debug.LogWarning($"received unknown HTTP request: {x}");
        }
    }


    private void HandleRequestLogin(NameValueCollection v)
    {
        var go = GameObject.Find("Networking");
        if (go == null)
        {
            Debug.LogWarning("login attempt but networking game object not found");
            return;
        }

        var host = v["host"] ?? "localhost";
        var portStr = v["port"] ?? "7979";
        if (!int.TryParse(portStr, out var port))
        {
            Debug.LogWarning($"login attempt with invalid port: {portStr}");
            return;
        }

        var playerIDStr = v["playerID"] ?? "0";
        var playerIDIsInt = int.TryParse(playerIDStr, out var playerID);
        if (!playerIDIsInt)
        {
            Debug.LogWarning($"login attempt with invalid playerID: {playerIDStr}");
            return;
        }

        var networking = go.GetComponent<Networking>();
        networking.LogIn(host, port, playerID);
    }

    private void HandleRequestBecomeThinClient(NameValueCollection v)
    {
        var host = v["host"] ?? "localhost";
        var addresses = Dns.GetHostAddresses(host);
        if (addresses.Length == 0)
        {
            Debug.LogWarning($"thinclient attempt with invalid host: {host}");
            return;
        }
        var renderIP = addresses.Where(x => x.AddressFamily == AddressFamily.InterNetwork).First();

        var portStr = v["port"] ?? "7980";
        if (!int.TryParse(portStr, out var port))
        {
            Debug.LogWarning($"thinclient attempt with invalid port: {portStr}");
            return;
        }

        var signalingPortStr = v["signalingPort"] ?? "7981";
        if (!int.TryParse(signalingPortStr, out var signalingPort))
        {
            Debug.LogWarning($"thinclient attempt with invalid signaling port: {signalingPortStr}");
            return;
        }

        var iceServers = ParseIceServers(v["iceServers"]);
        if (iceServers == null)
        {
            return;
        }

        var serverHostStr = v["serverHost"];
        var serverPortStr = v["serverPort"];
        IPEndPoint server = default;
        if (serverHostStr != null || serverPortStr != null)
        {
            IPAddress serverHost = default;
            if (serverHostStr != null)
            {
                var serverHosts = Dns.GetHostAddresses(serverHostStr);
                if (serverHosts.Length == 0)
                {
                    Debug.LogWarning($"could not parse serverHost: {serverHostStr}");
                    return;
                }
                serverHost = serverHosts
                    .Where(x => x.AddressFamily == AddressFamily.InterNetwork)
                    .First();
                if (serverHost == null)
                {
                    Debug.LogWarning($"could not find an IPv4 addr for serverHost: {serverHostStr}");
                    return;
                }
            }
            else
            {
                serverHost = gameManager.ServerEndpoint.Address;
            }

            int serverPort = default;
            if (serverPortStr != null)
            {
                if (!int.TryParse(serverPortStr, out serverPort))
                {
                    Debug.LogWarning($"could not parse serverPort: {serverPortStr}");
                    return;
                }
            }
            else
            {
                serverPort = gameManager.ServerEndpoint.Port;
            }

            server = new IPEndPoint(serverHost, serverPort);
        }
        else
        {
            server = gameManager.ServerEndpoint;
        }

        var render = new IPEndPoint(renderIP, port);
        var playerID = gameManager.PlayerID;
        StartCoroutine(BecomeThinClientRoutine(render, server, signalingPort, playerID, iceServers));
    }

    private IEnumerator BecomeThinClientRoutine(IPEndPoint render, IPEndPoint server, int signalingPort, uint playerID, IceServer[] iceServers)
    {
        var iceServerArr = iceServers.SelectMany(x => x.urls).ToArray();
        var iceServerStr = string.Join(",", iceServerArr);
        var uri = $"http://{render.Address}:{render.Port}/become/client" +
            $"?host={server.Address}" +
            $"&port={server.Port}" +
            $"&playerID={playerID}" +
            $"&broadcast={true}" +
            $"&signalingPort={signalingPort}" +
            $"&iceServers={iceServerStr}";
        Debug.Log(uri);

        UnityWebRequest request = null;
        yield return StartCoroutine(GetRequest(uri, value => request = value));

        using (request)
        {
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("HTTP Req Error: " + request.error);
                yield break;
            }
            else if (request.responseCode != 200)
            {
                Debug.LogWarning("Failed to tell renderer to become client: " + request.downloadHandler.text);
                yield break;
            }
        }

        yield return StartCoroutine(gameManager.SwitchSceneRoutine(GameScenes.ThinClient));

        ConnectToRenderer(new IPEndPoint(render.Address, signalingPort), iceServers);
    }

    private void ConnectToRenderer(IPEndPoint renderEndpoint, IceServer[] iceServers = null)
    {
        iceServers ??= new IceServer[] { new(urls: new[] { "stun: stun.l.google.com:19302" }) };
        var url = $"ws://{renderEndpoint}";
        var settings = new WebSocketSignalingSettings(url, iceServers);
        var receiver = FindObjectOfType<SignalingManager>();
        Assert.IsNotNull(receiver);
        Debug.Log($"connecting to signaling at {settings.url}");
        receiver.Run(settings);
    }

    IEnumerator GetRequest(string uri, Action<UnityWebRequest> callback)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        // Send the request and wait for it to complete
        yield return webRequest.SendWebRequest();
        callback(webRequest);
    }

    private void HandleRequestBecomeClient(NameValueCollection v)
    {
        StartCoroutine(HandleRequestBecomeClientRoutine(v));
    }

    private IEnumerator HandleRequestBecomeClientRoutine(NameValueCollection v)
    {
        // Start broadcast if requested
        var broadcastStr = v["broadcast"] ?? "false";
        if (!bool.TryParse(broadcastStr, out var broadcast))
        {
            Debug.LogWarning($"login attempt with invalid broadcast: {broadcastStr}");
            yield break;
        }

        // Make sure the Client scene is loaded
        yield return gameManager.SwitchSceneRoutine(GameScenes.Client);

        if (broadcast)
        {
            var hostStr = v["host"] ?? "localhost";
            var host = Dns.GetHostAddresses(hostStr)
                .Where(x => x.AddressFamily == AddressFamily.InterNetwork)
                .First();
            if (host == null)
            {
                Debug.LogWarning($"could not parse host: {hostStr}");
                yield break;
            }

            string signalingPortStr = v["signalingPort"] ?? "80";
            if (!int.TryParse(signalingPortStr, out var signalingPort))
            {
                yield break;
            }

            var endPoint = new IPEndPoint(host, signalingPort);

            var iceServers = ParseIceServers(v["iceServers"]);
            if (iceServers == null)
            {
                yield break;
            }

            ConnectToRenderer(endPoint, iceServers);
        }

        // Perform login
        HandleRequestLogin(v);
    }

    private IPEndPoint ParseSignalingServerEndpoint(string signalingServerStr)
    {
        signalingServerStr ??= "localhost:80";
        var signalingServerParts = signalingServerStr.Trim().Split(":");
        if (signalingServerParts.Length != 2)
        {
            Debug.LogWarning($"expecting host:port for signalingServer, but got: {signalingServerStr}");
            return null;
        }

        var IPStr = signalingServerParts[0];
        var signalingServerIP = Dns.GetHostAddresses(IPStr)
            .Where(x => x.AddressFamily == AddressFamily.InterNetwork)
            .First();
        if (signalingServerIP == null)
        {
            Debug.LogWarning($"could not resolve to IPv4 addr: {IPStr}");
            return null;
        }

        var portStr = signalingServerParts[1];
        if (!int.TryParse(portStr, out var port))
        {
            Debug.LogWarning($"expecting host:port for signalingServer, but could not parse port: {portStr}");
            return null;
        }

        var endpoint = new IPEndPoint(signalingServerIP, port);
        return endpoint;
    }

    private IceServer[] ParseIceServers(string iceServersStr)
    {
        iceServersStr ??= "stun:stun.l.google.com:19302";
        var iceServerParts = iceServersStr.Trim().Split(",");
        if (iceServerParts.Length < 1)
        {
            Debug.LogWarning($"want to broadcast but no ice servers");
            return null;
        }

        return iceServerParts
            .Select(x => new IceServer(urls: new[] { x }))
            .ToArray();
    }
}
