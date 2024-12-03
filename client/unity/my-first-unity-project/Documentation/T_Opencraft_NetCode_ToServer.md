# ToServer Class


Messages from clients to servers



## Definition
**Namespace:** <a href="N_Opencraft_NetCode.md">Opencraft.NetCode</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public sealed class ToServer : IMessage, 
	IMessage, IEquatable<ToServer>, IDeepCloneable
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  ToServer</td></tr>
<tr><td><strong>Implements</strong></td><td>IDeepCloneable, IMessage, IMessage, <a href="https://learn.microsoft.com/dotnet/api/system.iequatable-1" target="_blank" rel="noopener noreferrer">IEquatable</a>(ToServer)</td></tr>
</table>



## Constructors
<table>
<tr>
<td><a href="M_Opencraft_NetCode_ToServer__cctor.md">ToServer()</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToServer__ctor.md">ToServer()</a></td>
<td>Initializes a new instance of the ToServer class</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToServer__ctor_1.md">ToServer(ToServer)</a></td>
<td>Initializes a new instance of the ToServer class</td></tr>
</table>

## Properties
<table>
<tr>
<td><a href="P_Opencraft_NetCode_ToServer_Descriptor.md">Descriptor</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ToServer_IWantChangeBlock.md">IWantChangeBlock</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ToServer_IWantColumn.md">IWantColumn</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ToServer_IWantMovePlayer.md">IWantMovePlayer</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ToServer_IWantPlayer.md">IWantPlayer</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ToServer_Parser.md">Parser</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ToServer_PayloadCase.md">PayloadCase</a></td>
<td> </td></tr>
</table>

## Methods
<table>
<tr>
<td><a href="M_Opencraft_NetCode_ToServer_CalculateSize.md">CalculateSize</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToServer_ClearPayload.md">ClearPayload</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToServer_Clone.md">Clone</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToServer_Equals_1.md">Equals(Object)</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.equals#system-object-equals(system-object)" target="_blank" rel="noopener noreferrer">Object.Equals(Object)</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToServer_Equals.md">Equals(ToServer)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToServer_GetHashCode.md">GetHashCode</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.gethashcode" target="_blank" rel="noopener noreferrer">Object.GetHashCode()</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToServer_MergeFrom.md">MergeFrom(CodedInputStream)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToServer_MergeFrom_1.md">MergeFrom(ToServer)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToServer_ToString.md">ToString</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.tostring" target="_blank" rel="noopener noreferrer">Object.ToString()</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToServer_WriteTo.md">WriteTo</a></td>
<td> </td></tr>
</table>

## Fields
<table>
<tr>
<td><a href="F_Opencraft_NetCode_ToServer__parser.md">_parser</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToServer__unknownFields.md">_unknownFields</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToServer_IWantChangeBlockFieldNumber.md">IWantChangeBlockFieldNumber</a></td>
<td>Field number for the "i_want_change_block" field.</td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToServer_IWantColumnFieldNumber.md">IWantColumnFieldNumber</a></td>
<td>Field number for the "i_want_column" field.</td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToServer_IWantMovePlayerFieldNumber.md">IWantMovePlayerFieldNumber</a></td>
<td>Field number for the "i_want_move_player" field.</td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToServer_IWantPlayerFieldNumber.md">IWantPlayerFieldNumber</a></td>
<td>Field number for the "i_want_player" field.</td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToServer_payload_.md">payload_</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToServer_payloadCase_.md">payloadCase_</a></td>
<td> </td></tr>
</table>

## Explicit Interface Implementations
<table>
<tr>
<td><a href="P_Opencraft_NetCode_ToServer_pb__Google_Protobuf_IMessage_Descriptor.md">Descriptor</a></td>
<td> </td></tr>
</table>

## See Also


#### Reference
<a href="N_Opencraft_NetCode.md">Opencraft.NetCode Namespace</a>  
