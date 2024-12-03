# CommandLineInterface Class


A simple class used to define command-line options. Used in <a href="T_Bootstrap.md">Bootstrap</a>.



## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public class CommandLineInterface
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  CommandLineInterface</td></tr>
</table>



## Constructors
<table>
<tr>
<td><a href="M_CommandLineInterface__ctor.md">CommandLineInterface</a></td>
<td>Initializes a new instance of the CommandLineInterface class</td></tr>
</table>

## Properties
<table>
<tr>
<td><a href="P_CommandLineInterface_Hostname.md">Hostname</a></td>
<td>The server to which the client should connect. The provided string must be able to be parsed to an IPv4 address or a hostname.</td></tr>
<tr>
<td><a href="P_CommandLineInterface_HttpServerPort.md">HttpServerPort</a></td>
<td>The port on which this client's HTTP port is listening for incoming requests.</td></tr>
<tr>
<td><a href="P_CommandLineInterface_NoLogin.md">NoLogin</a></td>
<td>Prevents login. When set, client stays idle at startup.</td></tr>
<tr>
<td><a href="P_CommandLineInterface_Port.md">Port</a></td>
<td>The port on which to connect to the server.</td></tr>
<tr>
<td><a href="P_CommandLineInterface_UserID.md">UserID</a></td>
<td>The player ID with which to log in. A value of 0 lets the server assign an ID to this client. A value above 0 requests a login with that specific ID. Multiple clients can log in with the same ID to ease hand-overs when switching connections.</td></tr>
</table>

## See Also


#### Reference
<a href="N_.md">(Default Namespace) Namespace</a>  
