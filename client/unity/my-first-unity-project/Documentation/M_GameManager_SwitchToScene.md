# SwitchToScene Method


Switch the client to the given scene using an AsyncOperation, which you can check for completion. If you want to switch scenes in a Unity coroutine, you probably want to use the <a href="M_GameManager_SwitchSceneRoutine.md">SwitchSceneRoutine(GameScenes)</a> method instead.



## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public AsyncOperation SwitchToScene(
	GameScenes scene
)
```



#### Parameters
<dl><dt>  <a href="T_GameScenes.md">GameScenes</a></dt><dd>The scene to switch to.</dd></dl>

#### Return Value
AsyncOperation  
An AsyncOperation which you can use to check if the switch has completed.

## See Also


#### Reference
<a href="T_GameManager.md">GameManager Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
