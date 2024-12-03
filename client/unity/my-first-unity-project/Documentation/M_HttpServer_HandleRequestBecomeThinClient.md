# HandleRequestBecomeThinClient Method


Makes the client switch to the ThinClient scene and connect to another client which it will try to use as renderer. It will instruct the remote client to log in to the server to which this client is connected before becoming a thin client. 
The method pulls parameters from the provided dictionary to try to switch to a thin client. Accessed values are: <table><thead><tr><th>key</th><th>description</th></tr></thead><tr><td>host</td><td>A hostname or IPv4 address of the remote client (i.e., renderer) to connect to.</td></tr><tr><td>port</td><td>The port of the remote client (i.e., renderer) to connect to.</td></tr><tr><td>signalingPort</td><td>The port on which the signaling webserver is running. The webserver must run on the same location as specified by the <code>host</code> parameter.</td></tr><tr><td>iceServers</td><td>The ICE servers to use when trying to establish a direct WebRTC connection between the two clients (this thin client and the remote renderer client).</td></tr><tr><td>serverHost</td><td>An IPv4 address or hostname of the server the renderer should connect to. If not provided, the thin client will tell the renderer to connect to the same host the thin client was connected to.</td></tr><tr><td>serverPort</td><td>The port of the server the renderer should connect to. If not provided, the thin client will tell the renderer to connect to the same port the thin client was connected to.</td></tr></table>






## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
private void HandleRequestBecomeThinClient(
	NameValueCollection v
)
```



#### Parameters
<dl><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.collections.specialized.namevaluecollection" target="_blank" rel="noopener noreferrer">NameValueCollection</a></dt><dd>The dictionary with parameter values.</dd></dl>

## See Also


#### Reference
<a href="T_HttpServer.md">HttpServer Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
