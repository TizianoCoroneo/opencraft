# requests Field


Queue for incoming requests. New requests are enqueued by a receive thread, and handled by the main thread, once per frame.



## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
private ConcurrentQueue<HttpListenerContext> requests
```



#### Field Value
<a href="https://learn.microsoft.com/dotnet/api/system.collections.concurrent.concurrentqueue-1" target="_blank" rel="noopener noreferrer">ConcurrentQueue</a>(<a href="https://learn.microsoft.com/dotnet/api/system.net.httplistenercontext" target="_blank" rel="noopener noreferrer">HttpListenerContext</a>)

## See Also


#### Reference
<a href="T_HttpServer.md">HttpServer Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
<a href="F_HttpServer_listenLoop.md">listenLoop</a>  
<a href="M_HttpServer_ListenForRequests.md">ListenForRequests()</a>  
