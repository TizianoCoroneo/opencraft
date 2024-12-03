# UserInputManager Class


A scriptable object that manages possible user inputs.



## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public class UserInputManager : ScriptableObject
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  Object  →  ScriptableObject  →  UserInputManager</td></tr>
</table>



## Constructors
<table>
<tr>
<td><a href="M_UserInputManager__ctor.md">UserInputManager</a></td>
<td>Initializes a new instance of the UserInputManager class</td></tr>
</table>

## Methods
<table>
<tr>
<td><a href="M_UserInputManager_Awake.md">Awake</a></td>
<td> </td></tr>
<tr>
<td><a href="M_UserInputManager_OnDestroy.md">OnDestroy</a></td>
<td> </td></tr>
<tr>
<td><a href="M_UserInputManager_OnDisable.md">OnDisable</a></td>
<td>Disables user inputs.</td></tr>
<tr>
<td><a href="M_UserInputManager_OnEnable.md">OnEnable</a></td>
<td>Enables inputs and registers callbacks.</td></tr>
<tr>
<td><a href="M_UserInputManager_OnFire.md">OnFire</a></td>
<td>Invokes the callbacks registered on <a href="E_UserInputManager_FireEvent.md">FireEvent</a> when a fire input is received.</td></tr>
<tr>
<td><a href="M_UserInputManager_OnLook.md">OnLook</a></td>
<td>Invokes the callbacks registered on <a href="E_UserInputManager_LookEvent.md">LookEvent</a> when a look input is received.</td></tr>
<tr>
<td><a href="M_UserInputManager_OnMove.md">OnMove</a></td>
<td>Invokes the callbacks registered on <a href="E_UserInputManager_MoveEvent.md">MoveEvent</a> when a move input is received.</td></tr>
<tr>
<td><a href="M_UserInputManager_OnToggleThinClient.md">OnToggleThinClient</a></td>
<td>Toggles this client between being a regular client and a thin client when a <a href="E_UserInputManager_ToggleThinClientEvent.md">ToggleThinClientEvent</a> input is received.<br /><strong>Obsolete.</strong></td></tr>
</table>

## Events
<table>
<tr>
<td><a href="E_UserInputManager_FireEvent.md">FireEvent</a></td>
<td>Represents an input that interacts with the object the camera is currently looking at, or fires a weapon in the current direction.</td></tr>
<tr>
<td><a href="E_UserInputManager_LookEvent.md">LookEvent</a></td>
<td>Represents an input that changes the orientation of the user's camera.</td></tr>
<tr>
<td><a href="E_UserInputManager_MoveEvent.md">MoveEvent</a></td>
<td>Represents an input that changes the user's avatar location.</td></tr>
<tr>
<td><a href="E_UserInputManager_ToggleThinClientEvent.md">ToggleThinClientEvent</a></td>
<td>Represents an input that makes the client switch between being a regular client and a thin client.<br /><strong>Obsolete.</strong></td></tr>
</table>

## Fields
<table>
<tr>
<td><a href="F_UserInputManager_gameInput.md">gameInput</a></td>
<td>Can be queried for player inputs and can be used to register callbacks on certain inputs.</td></tr>
</table>

## See Also


#### Reference
<a href="N_.md">(Default Namespace) Namespace</a>  
