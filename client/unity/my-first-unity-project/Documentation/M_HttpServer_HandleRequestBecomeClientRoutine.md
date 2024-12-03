# HandleRequestBecomeClientRoutine Method


Switches from thin client to regular client and logs in at the specified server. 
This method is used to create either a local client (interacting directly with a user, inputs, and a screen, when `broadcast` is set to `false`), or a render client (broadcasting video frames and hidden behind a thin client, when `broadcast` is set to `true`).

The method pulls parameters from the provided dictionary to make the login attempt. Accessed values are: <ol><li><strong>host</strong> – A hostname or IPv4 address of the server to connect to. If <code>broadcast</code> is set to <code>true</code>, this is also assumed to be the address of the signaling server.</li><li><strong>port</strong> – The port of the server to connect to.</li><li><strong>playerID</strong> – The player ID with which to log in.</li><li><strong>broadcast</strong> – A flag indicating that the client should enable broadcasting video frames to any thin client that requests it.</li><li><strong>signalingPort</strong> – The port of the signaling webserver.</li><li><strong>playerID</strong> – The player ID with which to log in.</li><li><strong>iceServers</strong> – The ICE servers to use when trying to establish a direct WebRTC connection between the thin client and render client.</li></ol>






## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
private IEnumerator HandleRequestBecomeClientRoutine(
	NameValueCollection v
)
```



#### Parameters
<dl><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.collections.specialized.namevaluecollection" target="_blank" rel="noopener noreferrer">NameValueCollection</a></dt><dd>The dictionary with parameter values.</dd></dl>

#### Return Value
<a href="https://learn.microsoft.com/dotnet/api/system.collections.ienumerator" target="_blank" rel="noopener noreferrer">IEnumerator</a>  
\[Missing &lt;returns&gt; documentation for "M:HttpServer.HandleRequestBecomeClientRoutine(System.Collections.Specialized.NameValueCollection)"\]

## See Also


#### Reference
<a href="T_HttpServer.md">HttpServer Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
