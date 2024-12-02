using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using CommandLine;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Bootstrap is responsible for configuring the client networking correctly
/// when starting, such as connecting to a server at the correct address.
///
/// <para>
/// The <c>extraArguments</c> field allows users to use command-line arguments
/// when running the client via the editor. When using a standalone build, these
/// arguments are read from the command-line interface and the ones set in the
/// editor are ignored.
/// </para>
/// </summary>
/// <seealso cref="Networking"/>
/// <seealso cref="CommandLineInterface"/>
public class Bootstrap : MonoBehaviour
{
    [SerializeField] Networking networking;
    [SerializeField] private string[] extraArguments;

    /// <summary>
    /// Read-only command line arguments, as passed via the command line, or as
    /// extra arguments via the editor.
    /// </summary>
    public CommandLineInterface CommandLineArgs { get; private set; }

    /// <summary>
    /// Called when this script is enabled, before the first frame, connects the
    /// client to the configured server by parsing the command-line options
    /// provided via the editor (when in editor mode) or on the command line
    /// (when using a stand-alone build).
    ///
    /// </summary>
    /// <seealso
    /// href="https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html"/>
    void Awake()
    {
        var args = Environment.GetCommandLineArgs();
        var parser = new Parser(with =>
        {
            with.IgnoreUnknownArguments = true;
        });
#if UNITY_EDITOR
        var allArgs = args.Concat(extraArguments);
#else
        var allArgs = args;
#endif
        Debug.Log($"command line parameters: {string.Join(' ', allArgs)}");
        var result = parser.ParseArguments<CommandLineInterface>(allArgs);
        foreach (var error in result.Errors)
        {
            if (error is RepeatedOptionError repeatedError)
            {
                // Print the repeated option
                Debug.LogWarning($"Error: Option '{repeatedError.NameInfo.NameText}' was provided more than once.");
            }
            else
            {
                Debug.LogWarning(error.ToString());
            }
        }
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        CommandLineArgs = result.Value;
    }

    void Start()
    {
        RunOptions();
    }

    /// <summary>
    /// Starts the client in the way indicated by the provides command line options <c>opts</c>.
    /// </summary>
    private void RunOptions()
    {
        var opts = CommandLineArgs;

        if (opts.NoLogin)
        {
            return;
        }

        var addrs = Dns.GetHostAddresses(opts.Hostname);
        if (addrs.Length == 0)
        {
            Debug.LogError($"Could not parse hostname: {opts.Hostname}");
            return;
        }

        var ip = addrs.Where(x => x.AddressFamily == AddressFamily.InterNetwork).First();
        var ep = new IPEndPoint(ip, opts.Port);

        networking.LogIn(ep, opts.UserID);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
