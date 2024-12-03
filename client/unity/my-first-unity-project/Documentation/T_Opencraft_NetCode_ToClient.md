# ToClient Class


Messages from servers to clients



## Definition
**Namespace:** <a href="N_Opencraft_NetCode.md">Opencraft.NetCode</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public sealed class ToClient : IMessage, 
	IMessage, IEquatable<ToClient>, IDeepCloneable
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  ToClient</td></tr>
<tr><td><strong>Implements</strong></td><td>IDeepCloneable, IMessage, IMessage, <a href="https://learn.microsoft.com/dotnet/api/system.iequatable-1" target="_blank" rel="noopener noreferrer">IEquatable</a>(ToClient)</td></tr>
</table>



## Constructors
<table>
<tr>
<td><a href="M_Opencraft_NetCode_ToClient__cctor.md">ToClient()</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToClient__ctor.md">ToClient()</a></td>
<td>Initializes a new instance of the ToClient class</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToClient__ctor_1.md">ToClient(ToClient)</a></td>
<td>Initializes a new instance of the ToClient class</td></tr>
</table>

## Properties
<table>
<tr>
<td><a href="P_Opencraft_NetCode_ToClient_BlockUpdate.md">BlockUpdate</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ToClient_ChunkData.md">ChunkData</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ToClient_ColumnData.md">ColumnData</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ToClient_Descriptor.md">Descriptor</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ToClient_MultiBlockUpdate.md">MultiBlockUpdate</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ToClient_Parser.md">Parser</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ToClient_PayloadCase.md">PayloadCase</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ToClient_PlayerUpdate.md">PlayerUpdate</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ToClient_YouArePlayer.md">YouArePlayer</a></td>
<td> </td></tr>
</table>

## Methods
<table>
<tr>
<td><a href="M_Opencraft_NetCode_ToClient_CalculateSize.md">CalculateSize</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToClient_ClearPayload.md">ClearPayload</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToClient_Clone.md">Clone</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToClient_Equals_1.md">Equals(Object)</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.equals#system-object-equals(system-object)" target="_blank" rel="noopener noreferrer">Object.Equals(Object)</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToClient_Equals.md">Equals(ToClient)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToClient_GetHashCode.md">GetHashCode</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.gethashcode" target="_blank" rel="noopener noreferrer">Object.GetHashCode()</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToClient_MergeFrom.md">MergeFrom(CodedInputStream)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToClient_MergeFrom_1.md">MergeFrom(ToClient)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToClient_ToString.md">ToString</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.tostring" target="_blank" rel="noopener noreferrer">Object.ToString()</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ToClient_WriteTo.md">WriteTo</a></td>
<td> </td></tr>
</table>

## Fields
<table>
<tr>
<td><a href="F_Opencraft_NetCode_ToClient__parser.md">_parser</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToClient__unknownFields.md">_unknownFields</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToClient_BlockUpdateFieldNumber.md">BlockUpdateFieldNumber</a></td>
<td>Field number for the "block_update" field.</td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToClient_ChunkDataFieldNumber.md">ChunkDataFieldNumber</a></td>
<td>Field number for the "chunk_data" field.</td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToClient_ColumnDataFieldNumber.md">ColumnDataFieldNumber</a></td>
<td>Field number for the "column_data" field.</td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToClient_MultiBlockUpdateFieldNumber.md">MultiBlockUpdateFieldNumber</a></td>
<td>Field number for the "multi_block_update" field.</td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToClient_payload_.md">payload_</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToClient_payloadCase_.md">payloadCase_</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToClient_PlayerUpdateFieldNumber.md">PlayerUpdateFieldNumber</a></td>
<td>Field number for the "player_update" field.</td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ToClient_YouArePlayerFieldNumber.md">YouArePlayerFieldNumber</a></td>
<td>Field number for the "you_are_player" field.</td></tr>
</table>

## Explicit Interface Implementations
<table>
<tr>
<td><a href="P_Opencraft_NetCode_ToClient_pb__Google_Protobuf_IMessage_Descriptor.md">Descriptor</a></td>
<td> </td></tr>
</table>

## See Also


#### Reference
<a href="N_Opencraft_NetCode.md">Opencraft.NetCode Namespace</a>  
