# BecomeThinClientRoutine Method


Coroutine that switches the client to a thin client. This is a coroutine because it can take several frames to complete. By making it a coroutine, the main thread is not blocked.



## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
private IEnumerator BecomeThinClientRoutine(
	IPEndPoint render,
	IPEndPoint server,
	int signalingPort,
	uint playerID,
	IceServer[] iceServers
)
```



#### Parameters
<dl><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.net.ipendpoint" target="_blank" rel="noopener noreferrer">IPEndPoint</a></dt><dd>Endpoint (ip+port) of the rendering client.</dd><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.net.ipendpoint" target="_blank" rel="noopener noreferrer">IPEndPoint</a></dt><dd>Endpoint of the game server.</dd><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.int32" target="_blank" rel="noopener noreferrer">Int32</a></dt><dd>Port of the signaling webserver. The webserver must be available at the same IP address as <em>render</em>.</dd><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.uint32" target="_blank" rel="noopener noreferrer">UInt32</a></dt><dd>The player ID the rendering client should use to log in to the game server.</dd><dt>  IceServer[]</dt><dd>The ICE server(s) to use when trying to establish a connection for video frames and input between the thin client and the render client.</dd></dl>

#### Return Value
<a href="https://learn.microsoft.com/dotnet/api/system.collections.ienumerator" target="_blank" rel="noopener noreferrer">IEnumerator</a>  
\[Missing &lt;returns&gt; documentation for "M:HttpServer.BecomeThinClientRoutine(System.Net.IPEndPoint,System.Net.IPEndPoint,System.Int32,System.UInt32,Unity.RenderStreaming.IceServer[])"\]

## See Also


#### Reference
<a href="T_HttpServer.md">HttpServer Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
