# SwitchSceneRoutine Method


Switches the client to the given scene. This method can be run as a Unity Coroutine so that the switching can take place across multiple frames without freezing the main thread.



## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public IEnumerator SwitchSceneRoutine(
	GameScenes scene
)
```



#### Parameters
<dl><dt>  <a href="T_GameScenes.md">GameScenes</a></dt><dd>The scene to switch to.</dd></dl>

#### Return Value
<a href="https://learn.microsoft.com/dotnet/api/system.collections.ienumerator" target="_blank" rel="noopener noreferrer">IEnumerator</a>  
An enumerator used by Unity's Coroutine implementation to check if the operation has completed.

## See Also


#### Reference
<a href="T_GameManager.md">GameManager Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
