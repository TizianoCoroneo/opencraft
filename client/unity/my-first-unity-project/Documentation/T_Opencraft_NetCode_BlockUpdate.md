# BlockUpdate Class


\[Missing &lt;summary&gt; documentation for "T:Opencraft.NetCode.BlockUpdate"\]



## Definition
**Namespace:** <a href="N_Opencraft_NetCode.md">Opencraft.NetCode</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public sealed class BlockUpdate : IMessage, 
	IMessage, IEquatable<BlockUpdate>, IDeepCloneable
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  BlockUpdate</td></tr>
<tr><td><strong>Implements</strong></td><td>IDeepCloneable, IMessage, IMessage, <a href="https://learn.microsoft.com/dotnet/api/system.iequatable-1" target="_blank" rel="noopener noreferrer">IEquatable</a>(BlockUpdate)</td></tr>
</table>



## Constructors
<table>
<tr>
<td><a href="M_Opencraft_NetCode_BlockUpdate__cctor.md">BlockUpdate()</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_BlockUpdate__ctor.md">BlockUpdate()</a></td>
<td>Initializes a new instance of the BlockUpdate class</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_BlockUpdate__ctor_1.md">BlockUpdate(BlockUpdate)</a></td>
<td>Initializes a new instance of the BlockUpdate class</td></tr>
</table>

## Properties
<table>
<tr>
<td><a href="P_Opencraft_NetCode_BlockUpdate_BlockPosition.md">BlockPosition</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_BlockUpdate_BlockType.md">BlockType</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_BlockUpdate_Descriptor.md">Descriptor</a></td>
<td> </td></tr>
<tr>
<td><a href="P_Opencraft_NetCode_BlockUpdate_Parser.md">Parser</a></td>
<td> </td></tr>
</table>

## Methods
<table>
<tr>
<td><a href="M_Opencraft_NetCode_BlockUpdate_CalculateSize.md">CalculateSize</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_BlockUpdate_Clone.md">Clone</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_BlockUpdate_Equals.md">Equals(BlockUpdate)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_BlockUpdate_Equals_1.md">Equals(Object)</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.equals#system-object-equals(system-object)" target="_blank" rel="noopener noreferrer">Object.Equals(Object)</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_BlockUpdate_GetHashCode.md">GetHashCode</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.gethashcode" target="_blank" rel="noopener noreferrer">Object.GetHashCode()</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_BlockUpdate_MergeFrom_1.md">MergeFrom(BlockUpdate)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_BlockUpdate_MergeFrom.md">MergeFrom(CodedInputStream)</a></td>
<td> </td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_BlockUpdate_ToString.md">ToString</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.tostring" target="_blank" rel="noopener noreferrer">Object.ToString()</a>)</td></tr>
<tr>
<td><a href="M_Opencraft_NetCode_BlockUpdate_WriteTo.md">WriteTo</a></td>
<td> </td></tr>
</table>

## Fields
<table>
<tr>
<td><a href="F_Opencraft_NetCode_BlockUpdate__parser.md">_parser</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_BlockUpdate__unknownFields.md">_unknownFields</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_BlockUpdate_blockPosition_.md">blockPosition_</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_BlockUpdate_BlockPositionFieldNumber.md">BlockPositionFieldNumber</a></td>
<td>Field number for the "blockPosition" field.</td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_BlockUpdate_blockType_.md">blockType_</a></td>
<td> </td></tr>
<tr>
<td><a href="F_Opencraft_NetCode_BlockUpdate_BlockTypeFieldNumber.md">BlockTypeFieldNumber</a></td>
<td>Field number for the "blockType" field.</td></tr>
</table>

## Explicit Interface Implementations
<table>
<tr>
<td><a href="P_Opencraft_NetCode_BlockUpdate_pb__Google_Protobuf_IMessage_Descriptor.md">Descriptor</a></td>
<td> </td></tr>
</table>

## See Also


#### Reference
<a href="N_Opencraft_NetCode.md">Opencraft.NetCode Namespace</a>  
