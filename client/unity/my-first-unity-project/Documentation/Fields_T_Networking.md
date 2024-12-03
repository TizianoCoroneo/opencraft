# Networking Fields




## Fields
<table>
<tr>
<td><a href="F_Networking_automaticLogin.md">automaticLogin</a></td>
<td>When set to true, tries to automatically log in to a server running on localhost, using the default port of 7979, when the game starts. <p><b>DEPRECATED</b> this field is deprecated. Set to false. Use the <a href="T_Bootstrap.md">Bootstrap</a> class to connect to a server upon boot.</p><br /><strong>

Obsolete.</strong></td></tr>
<tr>
<td><a href="F_Networking_client.md">client</a></td>
<td>The client used to communicate with the game server.</td></tr>
<tr>
<td><a href="F_Networking_gameManager.md">gameManager</a></td>
<td>Reference to the <a href="T_GameManager.md">GameManager</a> ScriptableObjects, which maintains information about the (ongoing) game. This class uses the game manager to save the player ID received from the server and the server address after successfully logging in.</td></tr>
<tr>
<td><a href="F_Networking_messageQueue.md">messageQueue</a></td>
<td>A queue for incoming server messages, stored here when they are received but have not yet been processed.</td></tr>
<tr>
<td><a href="F_Networking_outgoingMessages.md">outgoingMessages</a></td>
<td>A queue for outgoing client messages, stored here when they have been generated but have not yet been sent using the <a href="F_Networking_client.md">client</a>.</td></tr>
<tr>
<td><a href="F_Networking_playerCharacter.md">playerCharacter</a></td>
<td>Reference to the player avatar object. Used to move the avatar to the correct location upon login. The field is set through the Unity editor.</td></tr>
<tr>
<td><a href="F_Networking_receiveLoop.md">receiveLoop</a></td>
<td>The task that reads incoming messages from <a href="https://learn.microsoft.com/dotnet/api/system.net.sockets.tcpclient" target="_blank" rel="noopener noreferrer">TcpClient</a> and enqueues them in the <a href="F_Networking_messageQueue.md">messageQueue</a>.</td></tr>
<tr>
<td><a href="F_Networking_running.md">running</a></td>
<td>Indicates whether there is an asynchronous task listening for incoming messages from the server.</td></tr>
<tr>
<td><a href="F_Networking_world.md">world</a></td>
<td>Reference to the game world. Used to load new chunks received from the server. The field is set through the Unity editor.</td></tr>
</table>

## See Also


#### Reference
<a href="T_Networking.md">Networking Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
