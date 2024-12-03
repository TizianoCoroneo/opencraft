# (Default Namespace) Namespace


\[Missing &lt;summary&gt; documentation for "N:"\]



## Classes
<table>
<tr>
<td><a href="T_Bootstrap.md">Bootstrap</a></td>
<td>Bootstrap is responsible for configuring the client networking correctly when starting, such as connecting to a server at the correct address. <p>The <code>extraArguments</code> field allows users to use command-line arguments when running the client via the editor. When using a standalone build, these arguments are read from the command-line interface and the ones set in the editor are ignored.</p></td></tr>
<tr>
<td><a href="T_CommandLineInterface.md">CommandLineInterface</a></td>
<td>A simple class used to define command-line options. Used in <a href="T_Bootstrap.md">Bootstrap</a>.</td></tr>
<tr>
<td><a href="T_FrameRateManager.md">FrameRateManager</a></td>
<td>Only sets the target frame-rate of the game to 30 FPS.</td></tr>
<tr>
<td><a href="T_GameManager.md">GameManager</a></td>
<td>This class can switch the client's deployment. Examples include logging in as a client to a specified server, disconnecting, or switching from client to thin-client mode.</td></tr>
<tr>
<td><a href="T_HttpServer.md">HttpServer</a></td>
<td>An HTTP server that listents for incoming requests that tell the client what to do.</td></tr>
<tr>
<td><a href="T_Networking.md">Networking</a></td>
<td>This class takes care of the client-server networking. It sends packets to the server, and it receives and handles packets received from the server.</td></tr>
<tr>
<td><a href="T_playerscript.md">playerscript</a></td>
<td> </td></tr>
<tr>
<td><a href="T_Readme.md">Readme</a></td>
<td> </td></tr>
<tr>
<td><a href="T_Readme_Section.md">Readme.Section</a></td>
<td> </td></tr>
<tr>
<td><a href="T_Receiver.md">Receiver</a></td>
<td> </td></tr>
<tr>
<td><a href="T_UserInputManager.md">UserInputManager</a></td>
<td> </td></tr>
<tr>
<td><a href="T_World.md">World</a></td>
<td>A trivial class responsible for keeping track of the game's world state.</td></tr>
</table>

## Enumerations
<table>
<tr>
<td><a href="T_GameScenes.md">GameScenes</a></td>
<td>Enum to keep track of the client's scenes without having to rely on strings.</td></tr>
</table>