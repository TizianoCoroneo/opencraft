# ParseIceServers Method


Parse a string of comma-concatenated ICE servers into a list of objects.



## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
private IceServer[] ParseIceServers(
	string iceServersStr
)
```



#### Parameters
<dl><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">String</a></dt><dd>The string of one or several ICE servers, comma separated</dd></dl>

#### Return Value
IceServer[]  
A parsed list of the provided ICE servers, or a single Google STUN server if the given list was null.

## See Also


#### Reference
<a href="T_HttpServer.md">HttpServer Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
