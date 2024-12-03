# HttpServer Fields




## Fields
<table>
<tr>
<td><a href="F_HttpServer_gameManager.md">gameManager</a></td>
<td>A link to the <a href="T_GameManager.md">GameManager</a>, assigned through the editor. This is the object that performs the scene switch (see <a href="M_GameManager_SwitchToScene.md">SwitchToScene(GameScenes)</a>) and keeps track of game data such as the server IP+port and the player ID. <p><a href="T_GameManager.md">GameManager</a> is a ScriptableObject, which are used to hold all sorts of data that other components want to look up at runtime. The HttpServer user the GameManager to store and look up the player ID and the server endpoint. For example, when receiving a request to switch from a regular client to a thin client, the HttpServer will in turn use the GameManager to look up to which server we are currently connected, and send a request to the render client telling it to connect to the server in our name.</p></td></tr>
<tr>
<td><a href="F_HttpServer_httpListener.md">httpListener</a></td>
<td>The object that actually listens for and receives incoming requests.</td></tr>
<tr>
<td><a href="F_HttpServer_listening.md">listening</a></td>
<td>True when the HTTP server is actively listening for requests. When set to false, the listen loop will stop listening after its next request.</td></tr>
<tr>
<td><a href="F_HttpServer_listenLoop.md">listenLoop</a></td>
<td>The asynchronous task (i.e., loop) that receives incoming HTTP requests. Stored in a field so that it can be neatly interrupted and stopped when existing the game.</td></tr>
<tr>
<td><a href="F_HttpServer_requests.md">requests</a></td>
<td>Queue for incoming requests. New requests are enqueued by a receive thread, and handled by the main thread, once per frame.</td></tr>
</table>

## See Also


#### Reference
<a href="T_HttpServer.md">HttpServer Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
