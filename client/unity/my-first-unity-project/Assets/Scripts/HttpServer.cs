using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
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
        var portIsInt = int.TryParse(portStr, out var port);
        if (!portIsInt)
        {
            Debug.LogWarning($"thinclient attempt with invalid port: {portStr}");
            return;
        }

        var render = new IPEndPoint(renderIP, port);
        var server = gameManager.ServerEndpoint;
        var playerID = gameManager.PlayerID;
        StartCoroutine(BecomeThinClientRoutine(render, server, playerID));
    }

    private IEnumerator BecomeThinClientRoutine(IPEndPoint render, IPEndPoint server, uint playerID)
    {
        var uri = $"http://{render.Address}:{render.Port}/become/client" +
            $"?host={server.Address}" +
            $"&port={server.Port}" +
            $"&playerID={playerID}" +
            $"&broadcast={true}";
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

        ConnectToRenderer(render.Address);
    }

    private void ConnectToRenderer(IPAddress renderIP)
    {
        var url = $"ws://{renderIP}";
        var iceServers = new IceServer[] { new(urls: new[] { "stun:stun.l.google.com:19302" }) };
        var settings = new WebSocketSignalingSettings(url, iceServers);
        var receiver = FindObjectOfType<SignalingManager>();
        Assert.IsNotNull(receiver);
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
        }

        // Make sure the Client scene is loaded
        yield return gameManager.SwitchSceneRoutine(GameScenes.Client);

        if (broadcast)
        {
            ConnectToRenderer(IPAddress.Loopback);
        }

        // Perform login
        HandleRequestLogin(v);
    }
}
