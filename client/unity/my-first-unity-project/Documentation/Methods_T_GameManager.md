# GameManager Methods




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

## See Also


#### Reference
<a href="T_GameManager.md">GameManager Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
