# (Default Namespace) Namespace


\[Missing &lt;summary&gt; documentation for "N:"\]



## Classes
<table>
<tr>
<td><a href="954a360b-3270-69b5-a233-1b69cb117a2f">Bootstrap</a></td>
<td>Bootstrap is responsible for configuring the client networking correctly when starting, such as connecting to a server at the correct address. <p>The <code>extraArguments</code> field allows users to use command-line arguments when running the client via the editor. When using a standalone build, these arguments are read from the command-line interface and the ones set in the editor are ignored.</p></td></tr>
<tr>
<td><a href="4cb1e842-7944-5550-4f33-466ba071776f">CommandLineInterface</a></td>
<td>A simple class used to define command-line options. Used in <a href="954a360b-3270-69b5-a233-1b69cb117a2f">Bootstrap</a>.</td></tr>
<tr>
<td><a href="32168d45-4f2b-d336-c1ca-d45b53006c3f">FrameRateManager</a></td>
<td>Only sets the target frame-rate of the game to 30 FPS.</td></tr>
<tr>
<td><a href="f9069e26-9179-a2cf-1d8b-b14d5bab9083">GameManager</a></td>
<td>This class can switch the client's deployment. Examples include logging in as a client to a specified server, disconnecting, or switching from client to thin-client mode.</td></tr>
<tr>
<td><a href="7bbe6138-d443-4516-35a8-3b0d7f5f9eeb">HttpServer</a></td>
<td>An HTTP server that listents for incoming requests that tell the client what to do.</td></tr>
<tr>
<td><a href="21386a3b-ed4e-b6ff-752b-64550aeca77d">Networking</a></td>
<td>This class takes care of the client-server networking. It sends packets to the server, and it receives and handles packets received from the server.</td></tr>
<tr>
<td><a href="5ac2e1d4-24a6-210f-bfbb-aac33ea7424f">playerscript</a></td>
<td> </td></tr>
<tr>
<td><a href="42011e0d-a77d-7bc0-112a-537db8c84475">Readme</a></td>
<td> </td></tr>
<tr>
<td><a href="33c0b09e-cb8e-0069-894d-d17e43d7470c">Readme.Section</a></td>
<td> </td></tr>
<tr>
<td><a href="7067775c-b8dd-c10b-5f4d-1c6df418d145">Receiver</a></td>
<td> </td></tr>
<tr>
<td><a href="4896c6d3-5d23-6f6f-4e8c-133443da47c6">UserInputManager</a></td>
<td> </td></tr>
<tr>
<td><a href="d854eab5-dac8-5516-107c-b220e2621138">World</a></td>
<td>A trivial class responsible for keeping track of the game's world state.</td></tr>
</table>

## Enumerations
<table>
<tr>
<td><a href="82756cfc-82ea-4042-d033-d2bab70a0a41">GameScenes</a></td>
<td>Enum to keep track of the client's scenes without having to rely on strings.</td></tr>
</table>