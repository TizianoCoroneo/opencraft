# LogIn(String, Int32, Int32) Method


Log in to a server.



## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public void LogIn(
	string serverHost = "localhost",
	int port = 7979,
	int playerID = 0
)
```



#### Parameters
<dl><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">String</a>  (Optional)</dt><dd>Server address.</dd><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.int32" target="_blank" rel="noopener noreferrer">Int32</a>  (Optional)</dt><dd>Server port.</dd><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.int32" target="_blank" rel="noopener noreferrer">Int32</a>  (Optional)</dt><dd>Player ID. If 0, the server will assign us a player ID. If larger than 0, we request to log in as that player. A player can be logged in multiple times.</dd></dl>

## See Also


#### Reference
<a href="T_Networking.md">Networking Class</a>  
<a href="Overload_Networking_LogIn.md">LogIn Overload</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
