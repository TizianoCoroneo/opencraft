# messageQueue Field


A queue for incoming server messages, stored here when they are received but have not yet been processed.



## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
private ConcurrentQueue<(ToClient , TcpClient )> messageQueue
```



#### Field Value
<a href="https://learn.microsoft.com/dotnet/api/system.collections.concurrent.concurrentqueue-1" target="_blank" rel="noopener noreferrer">ConcurrentQueue</a>(<a href="https://learn.microsoft.com/dotnet/api/system.valuetuple-2" target="_blank" rel="noopener noreferrer">ValueTuple</a>(<a href="T_Opencraft_NetCode_ToClient.md">ToClient</a>, <a href="https://learn.microsoft.com/dotnet/api/system.net.sockets.tcpclient" target="_blank" rel="noopener noreferrer">TcpClient</a>))

## See Also


#### Reference
<a href="T_Networking.md">Networking Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
