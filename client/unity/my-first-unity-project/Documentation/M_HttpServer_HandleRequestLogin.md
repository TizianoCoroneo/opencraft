# HandleRequestLogin Method


Makes the client log in as a client at the specified server. 
The method pulls parameters from the provided dictionary to make the login attempt. Accessed values are: <table><thead><tr><th>key</th><th>description</th></tr></thead><tr><td>host</td><td>A hostname or IPv4 address of the server to connect to.</td></tr><tr><td>port</td><td>The port of the server to connect to.</td></tr><tr><td>playerID</td><td>The player ID with which to log in.</td></tr></table>






## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
private void HandleRequestLogin(
	NameValueCollection v
)
```



#### Parameters
<dl><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.collections.specialized.namevaluecollection" target="_blank" rel="noopener noreferrer">NameValueCollection</a></dt><dd>The dictionary with parameter values.</dd></dl>

## See Also


#### Reference
<a href="T_HttpServer.md">HttpServer Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
