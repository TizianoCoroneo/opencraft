using UnityEditor;
using System.Xml;
using System.Text;

public class CustomCSProjectProcessor : AssetPostprocessor
{
    // This method is called whenever Unity generates or regenerates the C# project files
    public static string OnGeneratedCSProject(string path, string content)
    {
        // Load the .csproj file as XML
        XmlDocument csprojDoc = new();
        csprojDoc.LoadXml(content);

        var propertyGroup = csprojDoc.SelectSingleNode("//Project/PropertyGroup");
        var generateDoc = csprojDoc.CreateElement("GenerateDocumentationFile");
        generateDoc.InnerText = "true";
        propertyGroup.AppendChild(generateDoc);

        var sb = new StringBuilder();
        var xws = new XmlWriterSettings();
        xws.OmitXmlDeclaration = true;
        xws.Indent = true;
        using var xw = XmlWriter.Create(sb, xws);
        csprojDoc.Save(xw);
        return sb.ToString();
    }
}
