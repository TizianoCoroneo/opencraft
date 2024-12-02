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

        // Create or find an existing PropertyGroup to add a custom setting
        var propertyGroup = csprojDoc.CreateElement("PropertyGroup");
        // <GenerateDocumentationFile>true</GenerateDocumentationFile>
        // <DocumentationFile>bin/Debug/Assembly-CSharp.xml</DocumentationFile>
        var generateDoc = csprojDoc.CreateElement("GenerateDocumentationFile");
        generateDoc.InnerText = "true";
        propertyGroup.AppendChild(generateDoc);

        var documentationFile = csprojDoc.CreateElement("DocumentationFile");
        documentationFile.InnerText = "Builds/Docs/Assembly-CSharp.xml";
        propertyGroup.AppendChild(documentationFile);

        var project = csprojDoc.SelectSingleNode("//Project");
        project.AppendChild(propertyGroup);

        // Save the modified .csproj back to disk
        // return csprojDoc.OuterXml;

        var sb = new StringBuilder();
        var xws = new XmlWriterSettings();
        xws.OmitXmlDeclaration = true;
        xws.Indent = true;
        using var xw = XmlWriter.Create(sb, xws);
        csprojDoc.Save(xw);
        return sb.ToString();
    }
}
