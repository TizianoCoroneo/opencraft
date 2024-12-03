# GameManager Class


This class keeps track of game data that other scripts need access to, and can also switch the client's deployment. Examples include logging in as a client to a specified server, disconnecting, or switching from client to thin-client mode. 
This being a ScriptableObject makes it a good place to take care of scene switching, because regular scripts (i.e., MonoBehaviors) are part of a scene, and therefore not a great place to take actions that work across scenes.

This scene only executes switching, it does not decide when to act. Other scripts can call into this object to make the switch happen. For example, switching between a thin client and regular client via a HTTP GET request makes the <a href="T_HttpServer.md">HttpServer</a> call into this object.




## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public class GameManager : ScriptableObject
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  Object  →  ScriptableObject  →  GameManager</td></tr>
</table>



## Constructors
<table>
<tr>
<td><a href="M_GameManager__ctor.md">GameManager</a></td>
<td>Initializes a new instance of the GameManager class</td></tr>
</table>

## Properties
<table>
<tr>
<td><a href="P_GameManager_PlayerID.md">PlayerID</a></td>
<td>The player this client represents.</td></tr>
<tr>
<td><a href="P_GameManager_ServerEndpoint.md">ServerEndpoint</a></td>
<td>The server endpoint, IP and port.</td></tr>
</table>

## Methods
<table>
<tr>
<td><a href="M_GameManager_OnDestroy.md">OnDestroy</a></td>
<td> </td></tr>
<tr>
<td><a href="M_GameManager_OnDisable.md">OnDisable</a></td>
<td>Called when this script is disabled. Removes registered callbacks.</td></tr>
<tr>
<td><a href="M_GameManager_OnEnable.md">OnEnable</a></td>
<td>Called when this script is enabled. <p>Registers an input callback to toggle between client and thin client mode, allowing the user to switch between these two modes by pressing a button. This can be helpful when testing or debugging.</p></td></tr>
<tr>
<td><a href="M_GameManager_SwitchSceneRoutine.md">SwitchSceneRoutine</a></td>
<td>Switches the client to the given scene. This method can be run as a Unity Coroutine so that the switching can take place across multiple frames without freezing the main thread.</td></tr>
<tr>
<td><a href="M_GameManager_SwitchToScene.md">SwitchToScene</a></td>
<td>Switch the client to the given scene using an AsyncOperation, which you can check for completion. If you want to switch scenes in a Unity coroutine, you probably want to use the <a href="M_GameManager_SwitchSceneRoutine.md">SwitchSceneRoutine(GameScenes)</a> method instead.</td></tr>
<tr>
<td><a href="M_GameManager_ToggleThinClient.md">ToggleThinClient</a></td>
<td>Toggles between a client and thin client, used for debugging/testing.</td></tr>
<tr>
<td><a href="M_GameManager_Update.md">Update</a></td>
<td> </td></tr>
</table>

## Fields
<table>
<tr>
<td><a href="F_GameManager_inputManager.md">inputManager</a></td>
<td>Reference to the <a href="T_UserInputManager.md">UserInputManager</a>. Used to register a callback to toggle between a regular client and a thin client.</td></tr>
</table>

## See Also


#### Reference
<a href="N_.md">(Default Namespace) Namespace</a>  
<a href="T_HttpServer.md">HttpServer</a>  
