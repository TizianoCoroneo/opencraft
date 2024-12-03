# gameManager Field


A link to the <a href="T_GameManager.md">GameManager</a>, assigned through the editor. This is the object that performs the scene switch (see <a href="M_GameManager_SwitchToScene.md">SwitchToScene(GameScenes)</a>) and keeps track of game data such as the server IP+port and the player ID. 
<a href="T_GameManager.md">GameManager</a> is a ScriptableObject, which are used to hold all sorts of data that other components want to look up at runtime. The HttpServer user the GameManager to store and look up the player ID and the server endpoint. For example, when receiving a request to switch from a regular client to a thin client, the HttpServer will in turn use the GameManager to look up to which server we are currently connected, and send a request to the render client telling it to connect to the server in our name.




## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
private GameManager gameManager
```



#### Field Value
<a href="T_GameManager.md">GameManager</a>

## See Also


#### Reference
<a href="T_HttpServer.md">HttpServer Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
