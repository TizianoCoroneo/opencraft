# Networking Methods




## Methods
<table>
<tr>
<td><a href="M_Networking_HandleMessage.md">HandleMessage</a></td>
<td>Handle a single incoming message from the server.</td></tr>
<tr>
<td><a href="M_Networking_HandleMessageColumnData.md">HandleMessageColumnData</a></td>
<td>Handle a server message containing a column of blocks in the world. We should create that chunk and show it to the player.</td></tr>
<tr>
<td><a href="M_Networking_HandleMessageLogin.md">HandleMessageLogin</a></td>
<td>Handle the server's reply to our <a href="M_Networking_LogIn_1.md">LogIn(String, Int32, Int32)</a> request. If the login was successful, we should receive a player ID.</td></tr>
<tr>
<td><a href="M_Networking_LogIn.md">LogIn(IPEndPoint, Int32)</a></td>
<td>Log in to a server.</td></tr>
<tr>
<td><a href="M_Networking_LogIn_1.md">LogIn(String, Int32, Int32)</a></td>
<td>Log in to a server.</td></tr>
<tr>
<td><a href="M_Networking_OnDestroy.md">OnDestroy</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Networking_ReInitSocket.md">ReInitSocket</a></td>
<td>Close our current socket and create a new socket to start sending and receiving messages from the provided endpoint.</td></tr>
<tr>
<td><a href="M_Networking_SendToServer.md">SendToServer</a></td>
<td>Enqueue a message to be send to the server. Will be sent when <a href="M_Networking_Update.md">Update()</a> is called.</td></tr>
<tr>
<td><a href="M_Networking_Start.md">Start</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Networking_StartSocketReceive.md">StartSocketReceive</a></td>
<td>Start a new thread that listens for incoming messages and enqueues them for later processing in our main game loop.</td></tr>
<tr>
<td><a href="M_Networking_StopSocketReceive.md">StopSocketReceive</a></td>
<td>Stop listening to and receiving packets from the server endpoint.</td></tr>
<tr>
<td><a href="M_Networking_Update.md">Update</a></td>
<td>Send all queued outgoing messages to the server, and process all queued incoming messages from the server.</td></tr>
</table>

## See Also


#### Reference
<a href="T_Networking.md">Networking Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
