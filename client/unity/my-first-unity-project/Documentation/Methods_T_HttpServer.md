# HttpServer Methods




## Methods
<table>
<tr>
<td><a href="M_HttpServer_Awake.md">Awake</a></td>
<td>Called when this script instance is being loaded. <p>Used to ensure that there is only one instance of this class, and that that instance does not get destroyed when switching between scenes.</p></td></tr>
<tr>
<td><a href="M_HttpServer_BecomeThinClientRoutine.md">BecomeThinClientRoutine</a></td>
<td>Coroutine that switches the client to a thin client. This is a coroutine because it can take several frames to complete. By making it a coroutine, the main thread is not blocked.</td></tr>
<tr>
<td><a href="M_HttpServer_ConnectToRenderer.md">ConnectToRenderer</a></td>
<td>Make the connection to a signaling server to facilitate video broadcast.</td></tr>
<tr>
<td><a href="M_HttpServer_GetRequest.md">GetRequest</a></td>
<td>Performs a HTTP GET request on the provided URL and calls the provided callback upon completion.</td></tr>
<tr>
<td><a href="M_HttpServer_HandleRequest.md">HandleRequest</a></td>
<td>Most important method of this class. Handles a single HTTP request. <p>Supported commands are: <table><thead><tr><th>command</th><th>description</th></tr></thead><tr><td>login</td><td>log in as a client to the specified server. See <a href="M_HttpServer_HandleRequestLogin.md">HandleRequestLogin(NameValueCollection)</a>.</td></tr><tr><td>become/thinclient</td><td>Switch to the ThinClient scene if necessary and connect to another client that renders frames. See <a href="M_HttpServer_HandleRequestBecomeThinClient.md">HandleRequestBecomeThinClient(NameValueCollection)</a>.</td></tr><tr><td>become/client</td><td>Swtich to the Client scene if necessary and connect to a server. See <a href="M_HttpServer_HandleRequestBecomeClient.md">HandleRequestBecomeClient(NameValueCollection)</a>.</td></tr></table>

</p></td></tr>
<tr>
<td><a href="M_HttpServer_HandleRequestBecomeClient.md">HandleRequestBecomeClient</a></td>
<td>Wrapper around the <a href="M_HttpServer_HandleRequestBecomeClientRoutine.md">HandleRequestBecomeClientRoutine(NameValueCollection)</a> coroutine.</td></tr>
<tr>
<td><a href="M_HttpServer_HandleRequestBecomeClientRoutine.md">HandleRequestBecomeClientRoutine</a></td>
<td>Switches from thin client to regular client and logs in at the specified server. <p>This method is used to create either a local client (interacting directly with a user, inputs, and a screen, when <code>broadcast</code> is set to <code>false</code>), or a render client (broadcasting video frames and hidden behind a thin client, when <code>broadcast</code> is set to <code>true</code>).</p><p>

The method pulls parameters from the provided dictionary to make the login attempt. Accessed values are: <ol><li><strong>host</strong> – A hostname or IPv4 address of the server to connect to. If <code>broadcast</code> is set to <code>true</code>, this is also assumed to be the address of the signaling server.</li><li><strong>port</strong> – The port of the server to connect to.</li><li><strong>playerID</strong> – The player ID with which to log in.</li><li><strong>broadcast</strong> – A flag indicating that the client should enable broadcasting video frames to any thin client that requests it.</li><li><strong>signalingPort</strong> – <em>(REQUIRED ONLY WHEN broadcast == true)</em> The port of the signaling webserver.</li><li><strong>iceServers</strong> – <em>(REQUIRED ONLY WHEN broadcast == true)</em> The ICE servers to use when trying to establish a direct WebRTC connection between the thin client and render client.</li></ol>

</p></td></tr>
<tr>
<td><a href="M_HttpServer_HandleRequestBecomeThinClient.md">HandleRequestBecomeThinClient</a></td>
<td>Makes the client switch to the ThinClient scene and connect to another client, which it will instruct to log in to a server become a renderer. <p>If no serverHost and serverPort are provided, it will instruct the remote client to log in to the server to which this client is connected before becoming a thin client.</p><p>

The method pulls parameters from the provided dictionary to try to switch to a thin client. Accessed values are: <table><thead><tr><th>key</th><th>description</th></tr></thead><tr><td>host</td><td>A hostname or IPv4 address of the remote client (i.e., renderer) to connect to.</td></tr><tr><td>port</td><td>The port of the HTTP command server running on the render client, to which to this client can send a command.</td></tr><tr><td>signalingPort</td><td>The port on which the signaling webserver is running on the render client. The webserver must run on the same location as specified by the <code>host</code> parameter.</td></tr><tr><td>iceServers</td><td><em>(OPTIONAL)</em> The ICE servers to use when trying to establish a direct WebRTC connection between the two clients (this thin client and the remote renderer client).</td></tr><tr><td>serverHost</td><td><em>(OPTIONAL)</em> An IPv4 address or hostname of the server the renderer should connect to. If not provided, the thin client will tell the renderer to connect to the same host the thin client was connected to.</td></tr><tr><td>serverPort</td><td><em>(OPTIONAL)</em> The port of the server the renderer should connect to. If not provided, the thin client will tell the renderer to connect to the same port the thin client was connected to.</td></tr></table>

</p></td></tr>
<tr>
<td><a href="M_HttpServer_HandleRequestLogin.md">HandleRequestLogin</a></td>
<td>Makes the client log in as a client at the specified server. <p>The method pulls parameters from the provided dictionary to make the login attempt. Accessed values are: <table><thead><tr><th>key</th><th>description</th></tr></thead><tr><td>host</td><td>A hostname or IPv4 address of the server to connect to.</td></tr><tr><td>port</td><td>The port of the server to connect to.</td></tr><tr><td>playerID</td><td>The player ID with which to log in.</td></tr></table>

</p></td></tr>
<tr>
<td><a href="M_HttpServer_ListenForRequests.md">ListenForRequests</a></td>
<td>Listen for incoming requests and enqueue them. This method runs in a separate thread, mostly hanging on <a href="F_HttpServer_httpListener.md">httpListener</a>'s GetContext method.</td></tr>
<tr>
<td><a href="M_HttpServer_OnDisable.md">OnDisable</a></td>
<td>Stop the HTTP server when this script is disabled (e.g., when exiting play mode or stopping the process).</td></tr>
<tr>
<td><a href="M_HttpServer_ParseIceServers.md">ParseIceServers</a></td>
<td>Parse a string of comma-concatenated ICE servers into a list of objects.</td></tr>
<tr>
<td><a href="M_HttpServer_ParseSignalingServerEndpoint.md">ParseSignalingServerEndpoint</a></td>
<td>Parses the given string and returns a corresponding <a href="https://learn.microsoft.com/dotnet/api/system.net.ipendpoint" target="_blank" rel="noopener noreferrer">IPEndPoint</a>. If the parameter is null, the function returns an endpoint corresponding to <code>localhost:80</code></td></tr>
<tr>
<td><a href="M_HttpServer_Start.md">Start</a></td>
<td>Starts the HTTP Server when this script is enabled (e.g., when entering play mode or stopping the process).</td></tr>
<tr>
<td><a href="M_HttpServer_StartHttpServer.md">StartHttpServer</a></td>
<td>Start the HTTP server. Called by <a href="M_HttpServer_Start.md">Start()</a>.</td></tr>
<tr>
<td><a href="M_HttpServer_StopHttpServer.md">StopHttpServer</a></td>
<td>Stop the HTTP server. Called by <a href="M_HttpServer_OnDisable.md">OnDisable()</a>.</td></tr>
<tr>
<td><a href="M_HttpServer_Update.md">Update</a></td>
<td>Counts the number of HTTP requests in the queue and processes all of them. <p>TODO some protection against too many incoming requests, to avoid freezing the main thread.</p></td></tr>
</table>

## See Also


#### Reference
<a href="T_HttpServer.md">HttpServer Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
