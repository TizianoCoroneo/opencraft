# ParseSignalingServerEndpoint Method


Parses the given string and returns a corresponding <a href="https://learn.microsoft.com/dotnet/api/system.net.ipendpoint" target="_blank" rel="noopener noreferrer">IPEndPoint</a>. If the parameter is null, the function returns an endpoint corresponding to `localhost:80`



## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
private IPEndPoint ParseSignalingServerEndpoint(
	string signalingServerStr
)
```



#### Parameters
<dl><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">String</a></dt><dd>The string to parse.</dd></dl>

#### Return Value
<a href="https://learn.microsoft.com/dotnet/api/system.net.ipendpoint" target="_blank" rel="noopener noreferrer">IPEndPoint</a>  
The parsed IPEndPoint

## See Also


#### Reference
<a href="T_HttpServer.md">HttpServer Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
