# HandleRequest Method


Most important method of this class. Handles a single HTTP request. 
Supported commands are: <table><thead><tr><th>command</th><th>description</th></tr></thead><tr><td>login</td><td>log in as a client to the specified server. See <a href="M_HttpServer_HandleRequestLogin.md">HandleRequestLogin(NameValueCollection)</a>.</td></tr><tr><td>become/thinclient</td><td>Switch to the ThinClient scene if necessary and connect to another client that renders frames. See <a href="M_HttpServer_HandleRequestBecomeThinClient.md">HandleRequestBecomeThinClient(NameValueCollection)</a>.</td></tr><tr><td>become/client</td><td>Swtich to the Client scene if necessary and connect to a server. See <a href="M_HttpServer_HandleRequestBecomeClient.md">HandleRequestBecomeClient(NameValueCollection)</a>.</td></tr></table>






## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
private void HandleRequest(
	HttpListenerContext context
)
```



#### Parameters
<dl><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.net.httplistenercontext" target="_blank" rel="noopener noreferrer">HttpListenerContext</a></dt><dd>The request to handle.</dd></dl>

## See Also


#### Reference
<a href="T_HttpServer.md">HttpServer Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
