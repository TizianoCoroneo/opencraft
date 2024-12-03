# ListenPort Property


The port on which the HTTP server listens for requests. Given that this server is not meant to serve (HTML) content, it is better to use a custom port, and stay away from 80, 8080, and the likes. The default value for this port can be found in the <a href="T_CommandLineInterface.md">CommandLineInterface</a> class.



## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public int ListenPort { get; private set; }
```



#### Property Value
<a href="https://learn.microsoft.com/dotnet/api/system.int32" target="_blank" rel="noopener noreferrer">Int32</a>

## See Also


#### Reference
<a href="T_HttpServer.md">HttpServer Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
