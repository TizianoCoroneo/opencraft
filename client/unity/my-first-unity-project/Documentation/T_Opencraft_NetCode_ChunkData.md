# ChunkData Class


\[Missing &lt;summary&gt; documentation for "T:Opencraft.NetCode.ChunkData"\]



## Definition
**Namespace:** <a href="N_Opencraft_NetCode.md">Opencraft.NetCode</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public sealed class ChunkData : IMessage, 
	IMessage, IEquatable<ChunkData>, IDeepCloneable
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  ChunkData</td></tr>
<tr><td><strong>Implements</strong></td><td>IDeepCloneable, IMessage, IMessage, <a href="https://learn.microsoft.com/dotnet/api/system.iequatable-1" target="_blank" rel="noopener noreferrer">IEquatable</a>(ChunkData)</td></tr>
</table>



## Constructors
<table>
<tr>
<td><a href="M_Opencraft_NetCode_ChunkData__cctor.md">ChunkData()</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ChunkData__ctor.md">ChunkData()</a></td>
<td>Initializes a new instance of the ChunkData class</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ChunkData__ctor_1.md">ChunkData(ChunkData)</a></td>
<td>Initializes a new instance of the ChunkData class</td></tr>
</table>

## Properties
<table>
<tr>
<td><a href="P_Opencraft_NetCode_ChunkData_BlockTypes.md">BlockTypes</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ChunkData_Descriptor.md">Descriptor</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_ChunkData_Parser.md">Parser</a></td>
<td> </td></tr>
</table>

## Methods
<table>
<tr>
<td><a href="M_Opencraft_NetCode_ChunkData_CalculateSize.md">CalculateSize</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ChunkData_Clone.md">Clone</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ChunkData_Equals.md">Equals(ChunkData)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ChunkData_Equals_1.md">Equals(Object)</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.equals#system-object-equals(system-object)" target="_blank" rel="noopener noreferrer">Object.Equals(Object)</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ChunkData_GetHashCode.md">GetHashCode</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.gethashcode" target="_blank" rel="noopener noreferrer">Object.GetHashCode()</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ChunkData_MergeFrom_1.md">MergeFrom(ChunkData)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ChunkData_MergeFrom.md">MergeFrom(CodedInputStream)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ChunkData_ToString.md">ToString</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.tostring" target="_blank" rel="noopener noreferrer">Object.ToString()</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_ChunkData_WriteTo.md">WriteTo</a></td>
<td> </td></tr>
</table>

## Fields
<table>
<tr>
<td><a href="F_Opencraft_NetCode_ChunkData__parser.md">_parser</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ChunkData__unknownFields.md">_unknownFields</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ChunkData_blockTypes_.md">blockTypes_</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_ChunkData_BlockTypesFieldNumber.md">BlockTypesFieldNumber</a></td>
<td>Field number for the "blockTypes" field.</td></tr>
</table>

## Explicit Interface Implementations
<table>
<tr>
<td><a href="P_Opencraft_NetCode_ChunkData_pb__Google_Protobuf_IMessage_Descriptor.md">Descriptor</a></td>
<td> </td></tr>
</table>

## See Also


#### Reference
<a href="N_Opencraft_NetCode.md">Opencraft.NetCode Namespace</a>  
