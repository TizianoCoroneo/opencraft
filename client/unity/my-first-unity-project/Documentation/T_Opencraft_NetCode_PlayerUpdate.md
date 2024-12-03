# PlayerUpdate Class


\[Missing &lt;summary&gt; documentation for "T:Opencraft.NetCode.PlayerUpdate"\]



## Definition
**Namespace:** <a href="N_Opencraft_NetCode.md">Opencraft.NetCode</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public sealed class PlayerUpdate : IMessage, 
	IMessage, IEquatable<PlayerUpdate>, IDeepCloneable
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  PlayerUpdate</td></tr>
<tr><td><strong>Implements</strong></td><td>IDeepCloneable, IMessage, IMessage, <a href="https://learn.microsoft.com/dotnet/api/system.iequatable-1" target="_blank" rel="noopener noreferrer">IEquatable</a>(PlayerUpdate)</td></tr>
</table>



## Constructors
<table>
<tr>
<td><a href="M_Opencraft_NetCode_PlayerUpdate__cctor.md">PlayerUpdate()</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_PlayerUpdate__ctor.md">PlayerUpdate()</a></td>
<td>Initializes a new instance of the PlayerUpdate class</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_PlayerUpdate__ctor_1.md">PlayerUpdate(PlayerUpdate)</a></td>
<td>Initializes a new instance of the PlayerUpdate class</td></tr>
</table>

## Properties
<table>
<tr>
<td><a href="P_Opencraft_NetCode_PlayerUpdate_Descriptor.md">Descriptor</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_PlayerUpdate_Parser.md">Parser</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_PlayerUpdate_PlayerID.md">PlayerID</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_PlayerUpdate_Position.md">Position</a></td>
<td> </td></tr>
</table>

## Methods
<table>
<tr>
<td><a href="M_Opencraft_NetCode_PlayerUpdate_CalculateSize.md">CalculateSize</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_PlayerUpdate_Clone.md">Clone</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_PlayerUpdate_Equals_1.md">Equals(Object)</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.equals#system-object-equals(system-object)" target="_blank" rel="noopener noreferrer">Object.Equals(Object)</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_PlayerUpdate_Equals.md">Equals(PlayerUpdate)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_PlayerUpdate_GetHashCode.md">GetHashCode</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.gethashcode" target="_blank" rel="noopener noreferrer">Object.GetHashCode()</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_PlayerUpdate_MergeFrom.md">MergeFrom(CodedInputStream)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_PlayerUpdate_MergeFrom_1.md">MergeFrom(PlayerUpdate)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_PlayerUpdate_ToString.md">ToString</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.tostring" target="_blank" rel="noopener noreferrer">Object.ToString()</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_PlayerUpdate_WriteTo.md">WriteTo</a></td>
<td> </td></tr>
</table>

## Fields
<table>
<tr>
<td><a href="F_Opencraft_NetCode_PlayerUpdate__parser.md">_parser</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_PlayerUpdate__unknownFields.md">_unknownFields</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_PlayerUpdate_playerID_.md">playerID_</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_PlayerUpdate_PlayerIDFieldNumber.md">PlayerIDFieldNumber</a></td>
<td>Field number for the "playerID" field.</td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_PlayerUpdate_position_.md">position_</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_PlayerUpdate_PositionFieldNumber.md">PositionFieldNumber</a></td>
<td>Field number for the "position" field.</td></tr>
</table>

## Explicit Interface Implementations
<table>
<tr>
<td><a href="P_Opencraft_NetCode_PlayerUpdate_pb__Google_Protobuf_IMessage_Descriptor.md">Descriptor</a></td>
<td> </td></tr>
</table>

## See Also


#### Reference
<a href="N_Opencraft_NetCode.md">Opencraft.NetCode Namespace</a>  
