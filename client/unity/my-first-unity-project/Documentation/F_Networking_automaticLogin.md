# automaticLogin Field
<blockquote><strong>Note: This API is now obsolete.</strong></blockquote>




When set to true, tries to automatically log in to a server running on localhost, using the default port of 7979, when the game starts. 
**DEPRECATED** this field is deprecated. Set to false. Use the <a href="T_Bootstrap.md">Bootstrap</a> class to connect to a server upon boot.




## Definition
**Namespace:** <a href="N_.md">(Default Namespace)</a>  
**Assembly:** Assembly-CSharp (in Assembly-CSharp.dll) Version: 0.0.0.0

**C#**
``` C#
[ObsoleteAttribute("Logging in is now the responsibility of the Bootstrap class. Setting this value tries to log in on localhost.")]
private bool automaticLogin
```



#### Field Value
<a href="https://learn.microsoft.com/dotnet/api/system.boolean" target="_blank" rel="noopener noreferrer">Boolean</a>

## See Also


#### Reference
<a href="T_Networking.md">Networking Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
