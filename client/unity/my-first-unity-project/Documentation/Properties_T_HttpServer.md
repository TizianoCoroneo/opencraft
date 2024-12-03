# HttpServer Properties




## Properties
<table>
<tr>
<td><a href="P_HttpServer_Instance.md">Instance</a></td>
<td>Contains a reference to an HttpServer object, used to implement the singleton pattern and make sure only one instance of the HttpServer is created in every client.</td></tr>
<tr>
<td><a href="P_HttpServer_ListenPort.md">ListenPort</a></td>
<td>The port on which the HTTP server listens for requests. Given that this server is not meant to serve (HTML) content, it is better to use a custom port, and stay away from 80, 8080, and the likes. The default value for this port can be found in the <a href="T_CommandLineInterface.md">CommandLineInterface</a> class.</td></tr>
</table>

## See Also


#### Reference
<a href="T_HttpServer.md">HttpServer Class</a>  
<a href="N_.md">(Default Namespace) Namespace</a>  
