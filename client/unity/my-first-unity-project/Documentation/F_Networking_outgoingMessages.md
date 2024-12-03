# outgoingMessages Field


A queue for outgoing client messages, stored here when they have been generated but have not yet been sent using the <a href="F_Networking_client.md">client</a>.



## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
private ConcurrentQueue<ToServer> outgoingMessages
```



#### Field Value
<a href="https://learn.microsoft.com/dotnet/api/system.collections.concurrent.concurrentqueue-1" target="_blank" rel="noopener noreferrer">ConcurrentQueue</a>(<a href="T_Opencraft_NetCode_ToServer.md">ToServer</a>)

## See Also


#### Reference
<a href="T_Networking.md">Networking Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
