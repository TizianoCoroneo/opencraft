# ColumnData Class


\[Missing &lt;summary&gt; documentation for "T:Opencraft.NetCode.ColumnData"\]



## Definition
**Namespace:** <a href="N_Opencraft_NetCode.md">Opencraft.NetCode</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public sealed class ColumnData : IMessage, 
	IMessage, IEquatable<ColumnData>, IDeepCloneable
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  ColumnData</td></tr>
<tr><td><strong>Implements</strong></td><td>IDeepCloneable, IMessage, IMessage, <a href="https://learn.microsoft.com/dotnet/api/system.iequatable-1" target="_blank" rel="noopener noreferrer">IEquatable</a>(ColumnData)</td></tr>
</table>



## Constructors
<table>
<tr>
<td><a href="M_Opencraft_NetCode_ColumnData__cctor.md">ColumnData()</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ColumnData__ctor.md">ColumnData()</a></td>
<td>Initializes a new instance of the ColumnData class</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ColumnData__ctor_1.md">ColumnData(ColumnData)</a></td>
<td>Initializes a new instance of the ColumnData class</td></tr>
</table>

## Properties
<table>
<tr>
<td><a href="P_Opencraft_NetCode_ColumnData_Chunks.md">Chunks</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ColumnData_Descriptor.md">Descriptor</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ColumnData_Parser.md">Parser</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ColumnData_Position.md">Position</a></td>
<td> </td></tr>
</table>

## Methods
<table>
<tr>
<td><a href="M_Opencraft_NetCode_ColumnData_CalculateSize.md">CalculateSize</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ColumnData_Clone.md">Clone</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ColumnData_Equals.md">Equals(ColumnData)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ColumnData_Equals_1.md">Equals(Object)</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.equals#system-object-equals(system-object)" target="_blank" rel="noopener noreferrer">Object.Equals(Object)</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ColumnData_GetHashCode.md">GetHashCode</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.gethashcode" target="_blank" rel="noopener noreferrer">Object.GetHashCode()</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ColumnData_MergeFrom.md">MergeFrom(CodedInputStream)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ColumnData_MergeFrom_1.md">MergeFrom(ColumnData)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ColumnData_ToString.md">ToString</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.tostring" target="_blank" rel="noopener noreferrer">Object.ToString()</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ColumnData_WriteTo.md">WriteTo</a></td>
<td> </td></tr>
</table>

## Fields
<table>
<tr>
<td><a href="F_Opencraft_NetCode_ColumnData__parser.md">_parser</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ColumnData__repeated_chunks_codec.md">_repeated_chunks_codec</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ColumnData__unknownFields.md">_unknownFields</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ColumnData_chunks_.md">chunks_</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ColumnData_ChunksFieldNumber.md">ChunksFieldNumber</a></td>
<td>Field number for the "chunks" field.</td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ColumnData_position_.md">position_</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ColumnData_PositionFieldNumber.md">PositionFieldNumber</a></td>
<td>Field number for the "position" field.</td></tr>
</table>

## Explicit Interface Implementations
<table>
<tr>
<td><a href="P_Opencraft_NetCode_ColumnData_pb__Google_Protobuf_IMessage_Descriptor.md">Descriptor</a></td>
<td> </td></tr>
</table>

## See Also


#### Reference
<a href="N_Opencraft_NetCode.md">Opencraft.NetCode Namespace</a>  
