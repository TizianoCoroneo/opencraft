# ConnectToRenderer Method


Make the connection to a signaling server to facilitate video broadcast.



## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
private void ConnectToRenderer(
	IPEndPoint renderEndpoint,
	IceServer[] iceServers = null
)
```



#### Parameters
<dl><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.net.ipendpoint" target="_blank" rel="noopener noreferrer">IPEndPoint</a></dt><dd>Endpoint (ip+port) of the signaling server.</dd><dt>  IceServer[]  (Optional)</dt><dd>ICE server(s) used to try to establish a WebRTC connection. If not provided, a Google STUN server will be used.</dd></dl>

## See Also


#### Reference
<a href="T_HttpServer.md">HttpServer Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
