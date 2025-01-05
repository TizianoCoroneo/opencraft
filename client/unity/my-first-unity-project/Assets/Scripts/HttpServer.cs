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

/// <summary>
/// HttpServer runs on clients, both on user clients and render clients, and
/// allows remote controlling the client.
///
/// <para>
/// This class is mainly used to allow dynamically changing the game's
/// deployment, by instructing this client to connect to a server or render
/// client.
/// </para>
///
/// <para>
/// You can send requests to this server manually or automate it, for example by
/// creating a new MonoBehavior that issues commands to this server.
/// </para>
/// </summary>
/// <seealso cref="gameManager"/>
/// <seealso cref="GameManager"/>
public class HttpServer : MonoBehaviour
{
    /// <summary>
    /// Contains a reference to an HttpServer object, used to implement the
    /// singleton pattern and make sure only one instance of the HttpServer is
    /// created in every client.
    /// </summary>
    /// <seealso cref="Awake"/>
    public static HttpServer Instance { get; private set; }

    /// <summary>
    /// A link to the <see cref="GameManager"/>, assigned through the editor.
    /// This is the object that performs the scene switch (see <see
    /// cref="GameManager.SwitchToScene"/>) and keeps track of game data such as
    /// the server IP+port and the player ID.
    ///
    /// <para>
    /// <see cref="GameManager"/> is a <see cref="ScriptableObject"/>, which are
    /// used to hold all sorts of data that other components want to look up at
    /// runtime. The HttpServer user the GameManager to store and look up the
    /// player ID and the server endpoint. For example, when receiving a request
    /// to switch from a regular client to a thin client, the HttpServer will in
    /// turn use the GameManager to look up to which server we are currently
    /// connected, and send a request to the render client telling it to connect
    /// to the server in our name.
    /// </para>
    /// </summary>
    [SerializeField] private GameManager gameManager;

    [SerializeField] private World world;

    /// <summary>
    /// The port on which the HTTP server listens for requests. Given that this
    /// server is not meant to serve (HTML) content, it is better to use a
    /// custom port, and stay away from 80, 8080, and the likes. The default
    /// value for this port can be found in the <see
    /// cref="CommandLineInterface"/> class.
    /// </summary>
    public int ListenPort { get; private set; }

    /// <summary>
    /// The object that actually listens for and receives incoming requests.
    /// </summary>
    private HttpListener httpListener = default;

    /// <summary>
    /// True when the HTTP server is actively listening for requests. When set
    /// to false, the listen loop will stop listening after its next request.
    /// </summary>
    /// <seealso cref="StartHttpServer"/>
    /// <seealso cref="StopHttpServer"/>
    /// <seealso cref="ListenForRequests"/>
    private bool listening = default;

    /// <summary>
    /// The asynchronous task (i.e., loop) that receives incoming HTTP requests.
    /// Stored in a field so that it can be neatly interrupted and stopped when
    /// existing the game.
    /// </summary>
    /// <seealso cref="listening"/>
    /// <seealso cref="StartHttpServer"/>
    /// <seealso cref="StopHttpServer"/>
    /// <seealso cref="ListenForRequests"/>
    private Task listenLoop = default;

    /// <summary>
    /// Queue for incoming requests. New requests are enqueued by a receive
    /// thread, and handled by the main thread, once per frame.
    /// </summary>
    /// <seealso cref="listenLoop"/>
    /// <seealso cref="ListenForRequests"/>
    private ConcurrentQueue<HttpListenerContext> requests = new();

    /// <summary>
    /// Called when this script instance is being loaded.
    ///
    /// <para>
    /// Used to ensure that there is only one instance of this class, and that
    /// that instance does not get destroyed when switching between scenes.
    /// </para>
    /// </summary>
    /// <seealso cref="Instance"/>
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

    /// <summary>
    /// Starts the HTTP Server when this script is enabled (e.g., when entering
    /// play mode or stopping the process).
    /// </summary>
    void Start()
    {
        var bootstrap = FindAnyObjectByType<Bootstrap>();
        ListenPort = bootstrap.CommandLineArgs.HttpServerPort;
        StartHttpServer();
    }

    /// <summary>
    /// Stop the HTTP server when this script is disabled (e.g., when exiting
    /// play mode or stopping the process).
    /// </summary>
    void OnDisable()
    {
        StopHttpServer();
    }

    /// <summary>
    /// Counts the number of HTTP requests in the queue and processes all of
    /// them.
    ///
    /// <para>
    /// TODO some protection against too many incoming requests, to avoid
    /// freezing the main thread.
    /// </para>
    /// </summary>
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

    /// <summary>
    /// Start the HTTP server. Called by <see cref="Start"/>.
    /// </summary>
    private void StartHttpServer()
    {
        Debug.Log($"Starting HTTP Server on port {ListenPort}");
        if (httpListener != null)
        {
            Debug.LogWarning("there is already an http server!");
            return;
        }
        listening = true;
        httpListener = new();
        httpListener.Prefixes.Add($"http://*:{ListenPort}/");
        foreach (var r in requests)
        {
            r.Response.Close();
        }
        requests.Clear();
        httpListener.Start();
        listenLoop = Task.Run(() => ListenForRequests());
    }

    /// <summary>
    /// Stop the HTTP server. Called by <see cref="OnDisable"/>.
    /// </summary>
    private async void StopHttpServer()
    {
        Debug.Log("Stopping HTTP server");
        listening = false;
        httpListener?.Close();
        httpListener = null;
        if (listenLoop != null)
            await listenLoop;
    }


    /// <summary>
    /// Listen for incoming requests and enqueue them. This method runs in a
    /// separate thread, mostly hanging on <see cref="httpListener"/>'s
    /// GetContext method.
    /// </summary>
    /// <seealso cref="listening"/>
    /// <seealso cref="listenLoop"/>
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

    /// <summary>
    /// Most important method of this class. Handles a single HTTP request.
    ///
    /// <para>
    /// Supported commands are:
    /// <list type="table">
    /// <listheader>
    /// <term>command</term>
    /// <description>description</description>
    /// </listheader>
    /// <item>
    ///     <term>login</term>
    ///     <description>log in as a client to the specified server. See <see
    ///     cref="HandleRequestLogin"/>.</description>
    /// </item>
    /// <item>
    ///     <term>become/thinclient</term>
    ///     <description>Switch to the ThinClient scene if necessary and connect
    ///     to another client that renders frames. See <see
    ///     cref="HandleRequestBecomeThinClient"/>.</description>
    /// </item>
    /// <item>
    ///     <term>become/client</term>
    ///     <description>Swtich to the Client scene if necessary and connect to
    ///     a server. See <see cref="HandleRequestBecomeClient"/>.</description>
    /// </item>
    /// </list>
    /// </para>
    /// </summary>
    /// <param name="context">The request to handle.</param>
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


    /// <summary>
    /// Makes the client log in as a client at the specified server.
    ///
    /// <para>
    /// The method pulls parameters from the provided dictionary to make the
    /// login attempt. Accessed values are:
    /// <list type="table">
    /// <listheader>
    /// <term>key</term>
    /// <description>description</description>
    /// </listheader>
    ///     <item>
    ///         <term>host</term>
    ///         <description>A hostname or IPv4 address of the server to connect
    ///         to.</description>
    ///     </item>
    ///     <item>
    ///         <term>port</term>
    ///         <description>The port of the server to connect to.</description>
    ///     </item>
    ///     <item>
    ///         <term>playerID</term>
    ///         <description>The player ID with which to log in.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// </summary>
    /// <param name="v">The dictionary with parameter values.</param>
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

    /// <summary>
    /// Makes the client switch to the ThinClient scene and connect to another
    /// client, which it will instruct to log in to a server become a renderer.
    ///
    /// <para>
    /// If no serverHost and serverPort are provided, it will instruct the
    /// remote client to log in to the server to which this client is connected
    /// before becoming a thin client.
    /// </para>
    ///
    /// <para>
    /// The method pulls parameters from the provided dictionary to try to
    /// switch to a thin client. Accessed values are:
    /// <list type="table">
    /// <listheader>
    /// <term>key</term>
    /// <description>description</description>
    /// </listheader>
    ///     <item>
    ///         <term>host</term>
    ///         <description>A hostname or IPv4 address of the remote client
    ///         (i.e., renderer) to connect to.</description>
    ///     </item>
    ///     <item>
    ///         <term>port</term>
    ///         <description>The port of the HTTP command server running on the
    ///         render client, to which to this client can send a
    ///         command.</description>
    ///     </item>
    ///     <item>
    ///         <term>signalingPort</term>
    ///         <description>The port on which the signaling webserver is
    ///         running on the render client. The webserver must run on the same
    ///         location as specified by the <c>host</c>
    ///         parameter.</description>
    ///     </item>
    ///     <item>
    ///         <term>iceServers</term>
    ///         <description>
    ///         <i>(OPTIONAL)</i> The ICE servers to use when trying to
    ///         establish a direct WebRTC connection between the two clients
    ///         (this thin client and the remote renderer client).
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>serverHost</term>
    ///         <description>
    ///         <i>(OPTIONAL)</i> An IPv4 address or hostname of the server the
    ///         renderer should connect to. If not provided, the thin client
    ///         will tell the renderer to connect to the same host the thin
    ///         client was connected to.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>serverPort</term>
    ///         <description>
    ///         <i>(OPTIONAL)</i> The port of the server the renderer should
    ///         connect to. If not provided, the thin client will tell the
    ///         renderer to connect to the same port the thin client was
    ///         connected to.
    ///         </description>
    ///     </item>
    /// </list>
    /// </para>
    /// </summary>
    /// <param name="v">The dictionary with parameter values.</param>
    /// <seealso cref="Networking"/>
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

    /// <summary>
    /// Coroutine that switches the client to a thin client. This is a coroutine
    /// because it can take several frames to complete. By making it a
    /// coroutine, the main thread is not blocked.
    /// </summary>
    /// <param name="render">Endpoint (ip+port) of the rendering client.</param>
    /// <param name="server">Endpoint of the game server.</param>
    /// <param name="signalingPort">Port of the signaling webserver running on
    /// the render client. The webserver must be available at the same IP
    /// address as <paramref name="render"/>.</param>
    /// <param name="playerID">The player ID the rendering client should use to
    /// log in to the game server.</param>
    /// <param name="iceServers">The ICE server(s) to use when trying to
    /// establish a connection for video frames and input between the thin
    /// client and the render client.</param>
    /// <returns></returns>
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

        world.ClearLoadedChunks();
    }

    /// <summary>
    /// Make the connection to a signaling server to facilitate video broadcast.
    /// </summary>
    /// <param name="renderEndpoint">Endpoint (ip+port) of the signaling
    /// server.</param>
    /// <param name="iceServers">ICE server(s) used to try to establish a WebRTC
    /// connection. If not provided, a Google STUN server will be used.</param>
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

    /// <summary>
    /// Performs a HTTP GET request on the provided URL and calls the provided
    /// callback upon completion.
    /// </summary>
    /// <param name="uri">The URL that specifies the request.</param>
    /// <param name="callback">The callback to call upon completion of the
    /// request.</param>
    /// <returns>IEnumerator to implement this as a coroutine.</returns>
    IEnumerator GetRequest(string uri, Action<UnityWebRequest> callback)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        // Send the request and wait for it to complete
        yield return webRequest.SendWebRequest();
        callback(webRequest);
    }

    /// <summary>
    /// Wrapper around the <see cref="HandleRequestBecomeClientRoutine"/>
    /// coroutine.
    /// </summary>
    /// <param name="v">A dictionary with required parameters.</param>
    /// <seealso cref="HandleRequestBecomeClientRoutine"/>
    private void HandleRequestBecomeClient(NameValueCollection v)
    {
        StartCoroutine(HandleRequestBecomeClientRoutine(v));
    }

    /// <summary>
    /// Switches from thin client to regular client and logs in at the specified
    /// server.
    ///
    /// <para>
    /// This method is used to create either a local client (interacting
    /// directly with a user, inputs, and a screen, when <c>broadcast</c> is set
    /// to <c>false</c>), or a render client (broadcasting video frames and
    /// hidden behind a thin client, when <c>broadcast</c> is set to
    /// <c>true</c>).
    /// </para>
    ///
    /// <para>
    /// The method pulls parameters from the provided dictionary to make the
    /// login attempt. Accessed values are:
    /// <list type="number">
    /// <listheader>
    /// <term>key</term>
    /// <description>description</description>
    /// </listheader>
    ///     <item>
    ///         <term>host</term>
    ///         <description>A hostname or IPv4 address of the server to connect
    ///         to. If <c>broadcast</c> is set to <c>true</c>, this is also
    ///         assumed to be the address of the signaling server.</description>
    ///     </item>
    ///     <item>
    ///         <term>port</term>
    ///         <description>The port of the server to connect to.</description>
    ///     </item>
    ///     <item>
    ///         <term>playerID</term>
    ///         <description>The player ID with which to log in.</description>
    ///     </item>
    ///     <item>
    ///         <term>broadcast</term>
    ///         <description>A flag indicating that the client should enable
    ///         broadcasting video frames to any thin client that requests
    ///         it.</description>
    ///     </item>
    ///     <item>
    ///         <term>signalingPort</term>
    ///         <description>
    ///         <i>(REQUIRED ONLY WHEN broadcast == true)</i> The port of the
    ///         signaling webserver.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>iceServers</term>
    ///         <description>
    ///         <i>(REQUIRED ONLY WHEN broadcast == true)</i> The ICE servers to
    ///         use when trying to establish a direct WebRTC connection between
    ///         the thin client and render client.
    ///         </description>
    ///     </item>
    /// </list>
    /// </para>
    /// </summary>
    /// <param name="v">The dictionary with parameter values.</param>
    private IEnumerator HandleRequestBecomeClientRoutine(NameValueCollection v)
    {
        // TODO if we are currently a thin client, tell the renderer to log out.

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

        world.ClearLoadedChunks();
    }

    /// <summary>
    /// Parses the given string and returns a corresponding <see
    /// cref="IPEndPoint"/>. If the parameter is null, the function returns an
    /// endpoint corresponding to <c>localhost:80</c>
    /// </summary>
    /// <param name="signalingServerStr">The string to parse.</param>
    /// <returns>The parsed IPEndPoint</returns>
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

    /// <summary>
    /// Parse a string of comma-concatenated ICE servers into a list of objects.
    /// </summary>
    /// <param name="iceServersStr">The string of one or several ICE servers,
    /// comma separated</param>
    /// <returns>A parsed list of the provided ICE servers, or a single Google
    /// STUN server if the given list was null.</returns>
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
