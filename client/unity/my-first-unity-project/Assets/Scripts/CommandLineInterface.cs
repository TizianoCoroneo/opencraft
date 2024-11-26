using CommandLine;

/// <summary>
/// A simple class used to define command-line options. Used in <see cref="Bootstrap"/>.
/// </summary>
public class CommandLineInterface
{
    /// <summary>
    /// The server to which the client should connect. The provided string must
    /// be able to be parsed to an IPv4 address or a hostname.
    /// </summary>
    [Option("host", Required = false, HelpText = "Server hostname")]
    public string Hostname { get; set; }

    /// <summary>
    /// The port on which to connect to the server.
    /// </summary>
    [Option("port", Default = 7979, Required = false, HelpText = "Server port")]
    public int Port { get; set; }

    /// <summary>
    /// The player ID with which to log in. A value of 0 lets the server assign
    /// an ID to this client. A value above 0 requests a login with that
    /// specific ID. Multiple clients can log in with the same ID to ease
    /// hand-overs when switching connections.
    /// </summary>
    [Option("user", Default = 0, Required = false, HelpText = "User ID")]
    public int UserID { get; set; }
}
