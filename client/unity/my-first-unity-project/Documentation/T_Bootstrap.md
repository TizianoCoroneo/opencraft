# Bootstrap Class


Bootstrap is responsible for configuring the client networking correctly when starting, such as connecting to a server at the correct address. 
The `extraArguments` field allows users to use command-line arguments when running the client via the editor. When using a standalone build, these arguments are read from the command-line interface and the ones set in the editor are ignored.




## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public class Bootstrap : MonoBehaviour
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  Object  →  Component  →  Behaviour  →  MonoBehaviour  →  Bootstrap</td></tr>
</table>



## Constructors
<table>
<tr>
<td><a href="M_Bootstrap__ctor.md">Bootstrap</a></td>
<td>Initializes a new instance of the Bootstrap class</td></tr>
</table>

## Properties
<table>
<tr>
<td><a href="P_Bootstrap_CommandLineArgs.md">CommandLineArgs</a></td>
<td>Read-only command line arguments, as passed via the command line, or as extra arguments via the editor.</td></tr>
</table>

## Methods
<table>
<tr>
<td><a href="M_Bootstrap_Awake.md">Awake</a></td>
<td>Called when this script is enabled, before the first frame, connects the client to the configured server by parsing the command-line options provided via the editor (when in editor mode) or on the command line (when using a stand-alone build).</td></tr>
<tr>
<td><a href="M_Bootstrap_RunOptions.md">RunOptions</a></td>
<td>Starts the client in the way indicated by the provides command line options <code>opts</code>.</td></tr>
<tr>
<td><a href="M_Bootstrap_Start.md">Start</a></td>
<td>Automatically called when the game starts. Calls <a href="M_Bootstrap_RunOptions.md">RunOptions()</a>.</td></tr>
<tr>
<td><a href="M_Bootstrap_Update.md">Update</a></td>
<td> </td></tr>
</table>

## Fields
<table>
<tr>
<td><a href="F_Bootstrap_extraArguments.md">extraArguments</a></td>
<td>A list of arguments, specified using the editor, that are interpreted as additional command line arguments. This field is only used when the game runs in the editor. Stand-alone builds ignore this field.</td></tr>
<tr>
<td><a href="F_Bootstrap_networking.md">networking</a></td>
<td>Instance of <a href="T_Networking.md">Networking</a>, responsible for networking between the client and server. Used in this class to call <a href="M_Networking_LogIn.md">LogIn(IPEndPoint, Int32)</a>.</td></tr>
</table>

## See Also


#### Reference
<a href="N_.md">(Default Namespace) Namespace</a>  
<a href="T_Networking.md">Networking</a>  
<a href="T_CommandLineInterface.md">CommandLineInterface</a>  
