using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using CommandLine;
using UnityEngine;
using UnityEngine.Assertions;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] Networking networking;
    [SerializeField] private string[] extraArguments;

    // Start is called before the first frame update
    void Start()
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
        RunOptions(result.Value);
    }

    private void RunOptions(CommandLineInterface opts)
    {
        if (string.IsNullOrEmpty(opts.Hostname))
        {
            Debug.LogWarning("no --host specified");
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
