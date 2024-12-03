# MultiBlockUpdate Class


\[Missing &lt;summary&gt; documentation for "T:Opencraft.NetCode.MultiBlockUpdate"\]



## Definition
**Namespace:** <a href="74916f57-cba9-5924-c41d-e8d2ff36e87a">Opencraft.NetCode</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
public sealed class MultiBlockUpdate : IMessage, 
	IMessage, IEquatable<MultiBlockUpdate>, IDeepCloneable
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  MultiBlockUpdate</td></tr>
<tr><td><strong>Implements</strong></td><td>IDeepCloneable, IMessage, IMessage, <a href="https://learn.microsoft.com/dotnet/api/system.iequatable-1" target="_blank" rel="noopener noreferrer">IEquatable</a>(MultiBlockUpdate)</td></tr>
</table>



## Constructors
<table>
<tr>
<td><a href="9b061cf8-b7b6-92d8-dd46-a464e085f7df">MultiBlockUpdate()</a></td>
<td>Initializes a new instance of the MultiBlockUpdate class</td></tr>
<tr>
<td><a href="729794ca-a442-ec8c-8c29-0b0ea9efbcbc">MultiBlockUpdate(MultiBlockUpdate)</a></td>
<td>Initializes a new instance of the MultiBlockUpdate class</td></tr>
</table>

## Properties
<table>
<tr>
<td><a href="cbec68fb-002f-4ec4-1530-6c9a6ee92e40">Descriptor</a></td>
<td> </td></tr>
<tr>
<td><a href="b13e3996-e844-6473-5585-f583d2a08f6f">Parser</a></td>
<td> </td></tr>
<tr>
<td><a href="4a25cb47-558d-3b95-ac90-300b7f5b0799">Updates</a></td>
<td> </td></tr>
</table>

## Methods
<table>
<tr>
<td><a href="6ea9910e-d52a-d36b-96a2-d1d20ea7aff0">CalculateSize</a></td>
<td> </td></tr>
<tr>
<td><a href="4d846fc9-e523-db85-0643-41bdb06ad795">Clone</a></td>
<td> </td></tr>
<tr>
<td><a href="9c3724ea-865d-3997-4b69-32145a6a184c">Equals(MultiBlockUpdate)</a></td>
<td> </td></tr>
<tr>
<td><a href="57d2a2e6-db2c-e0b2-c8f7-5158f4ed046c">Equals(Object)</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.equals#system-object-equals(system-object)" target="_blank" rel="noopener noreferrer">Object.Equals(Object)</a>)</td></tr>
<tr>
<td><a href="0d84c2ca-9313-7339-a39d-a92a4f9c9073">GetHashCode</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.gethashcode" target="_blank" rel="noopener noreferrer">Object.GetHashCode()</a>)</td></tr>
<tr>
<td><a href="https://learn.microsoft.com/dotnet/api/system.object.gettype" target="_blank" rel="noopener noreferrer">GetType</a></td>
<td>Gets the <a href="https://learn.microsoft.com/dotnet/api/system.type" target="_blank" rel="noopener noreferrer">Type</a> of the current instance.<br />(Inherited from <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>)</td></tr>
<tr>
<td><a href="ef29be4d-63ab-aefb-f420-051a24e55262">MergeFrom(CodedInputStream)</a></td>
<td> </td></tr>
<tr>
<td><a href="4ba9d7a9-2615-43ae-6591-ee4cf26a4e53">MergeFrom(MultiBlockUpdate)</a></td>
<td> </td></tr>
<tr>
<td><a href="c4dbc4a7-74b5-22b3-14c4-23771068a823">ToString</a></td>
<td><br />(Overrides <a href="https://learn.microsoft.com/dotnet/api/system.object.tostring" target="_blank" rel="noopener noreferrer">Object.ToString()</a>)</td></tr>
<tr>
<td><a href="ff6a4384-20ad-154b-fb75-0ff87179c00c">WriteTo</a></td>
<td> </td></tr>
</table>

## Fields
<table>
<tr>
<td><a href="1a00d957-ba21-5d2d-6f98-8e31a48070c1">UpdatesFieldNumber</a></td>
<td>Field number for the "updates" field.</td></tr>
</table>

## See Also


#### Reference
<a href="74916f57-cba9-5924-c41d-e8d2ff36e87a">Opencraft.NetCode Namespace</a>  
