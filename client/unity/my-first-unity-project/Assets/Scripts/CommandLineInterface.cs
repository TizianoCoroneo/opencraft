using CommandLine;

/// <summary>
/// A simple class used to define command-line options. Used in <see
/// cref="Bootstrap"/>.
/// </summary>
public class CommandLineInterface
{
    /// <summary>
    /// Prevents login. When set, client stays idle at startup.
    /// </summary>
    [Option("noLogin", Default = false, Required = false, HelpText = "Prevents login. When set, client stays idle at startup.")]
    public bool NoLogin { get; set; }

    /// <summary>
    /// The server to which the client should connect. The provided string must
    /// be able to be parsed to an IPv4 address or a hostname.
    /// </summary>
    [Option("host", Default = "localhost", Required = false, HelpText = "Server hostname")]
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

    /// <summary>
    /// The port on which this client's HTTP port is listening for incoming
    /// requests.
    /// </summary>
    /// <seealso cref="HttpServer"/>
    [Option("httpPort", Default = 7980, Required = false, HelpText = "Listen port for this client's HTTP server")]
    public int HttpServerPort { get; set; }
}
