using CommandLine;

public class CommandLineInterface
{
    [Option("host", Required = false, HelpText = "Server hostname")]
    public string Hostname { get; set; }

    [Option("port", Default = 7979, Required = false, HelpText = "Server port")]
    public int Port { get; set; }

    [Option("user", Default = 0, Required = false, HelpText = "User ID")]
    public int UserID { get; set; }
}
