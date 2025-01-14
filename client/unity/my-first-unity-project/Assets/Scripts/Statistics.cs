using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Opencraft.NetCode;
using System;
using Unity.VisualScripting;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

public class Statistics : MonoBehaviour
{
    /// <summary>
    /// Reference to the Networking component.
    /// </summary>
    [SerializeField] private MonoBehaviour networkComponent;
    private INetworking network;

    /// <summary>
    /// The current round-trip time.
    /// </summary>
    public float RTT { get; set; } = 60f;
    /// <summary>
    /// The current jitter.
    /// </summary>
    public float Jitter { get; set; } = 60f;
    /// <summary>
    /// The current frames per second.
    /// </summary>
    public float FPS { get; set; }
    /// <summary>
    /// The current CPU usage.
    /// </summary>
    public float CPU { get; set; }
    /// <summary>
    /// The current GPU usage.
    /// </summary>
    public float GPU { get; set; }
    /// <summary>
    /// The current memory usage.
    /// </summary>
    public float Memory { get; set; }
    /// <summary>
    /// The current battery watt usage.
    /// </summary>
    public float BatteryWatt { get; set; }

    [SerializeField] private float measurementGap = 1;
    [SerializeField] private bool isHomeSide = false;
    private string filePath = default;
    private readonly Stopwatch stopwatch = new();

    // Start is called before the first frame update
    void Start()
    {
        if (!isHomeSide)
            return;

        stopwatch.Start();
        network = (INetworking)networkComponent;

        // Initialize stats file
        filePath = Application.persistentDataPath + $"/stats/{DateTime.Now.ToString("o")}.csv";
        if (!File.Exists(filePath))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/stats");
            using StreamWriter writer = new(filePath, false);
            writer.WriteLine("RTT,FPS,CPU,GPU,Memory,BatteryWatt,Jitter");
            UnityEngine.Debug.Log("File initialized: " + filePath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHomeSide)
            return;

        // Update FPS
        FPS = 1 / Time.deltaTime;

        if (stopwatch.ElapsedMilliseconds > measurementGap * 1000)
        {
            SendPing();
            UpdateSystemStats();
            UnityEngine.Debug.Log($"RTT: {RTT} ms, FPS: {FPS}, CPU: {CPU}%, GPU: {GPU}%, Memory: {Memory}%, Battery Wattage: {BatteryWatt}%, Jitter: {Jitter} ms");
            WriteStats();
            stopwatch.Restart();
        }
    }

    private void SendPing()
    {
        network.SendToServer(new ToServer
        {
            IWantOpenPing = new IWantOpenPing { TimeSent = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() }
        });
    }

    private async void UpdateSystemStats()
    {
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            UnityEngine.Debug.Log("TODO: OSX commands here");
        }
        else if (Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.LinuxEditor)
        {
            var cpuTask = "mpstat -P 0 | awk 'FNR==4{print ($NF>0?100-$NF:$NF)}'";
            var gpuTask = "nvidia-smi --query-gpu=utilization.gpu --format=csv,noheader,nounits";
            var memoryTask = "free | grep Mem | awk '{print $3/$2 * 100.0}'";
            var batteryTask = "upower -i $(upower -e 'BAT') | grep -E 'energy-rate' | awk '{print $2}'";

            var result = await RunShellCommand(cpuTask + "; " + gpuTask + "; " + memoryTask + "; " + batteryTask);
            var results = result.Split("\n");

            CPU = float.Parse(results[0]);
            GPU = float.Parse(results[1]);
            Memory = float.Parse(results[2]);
            BatteryWatt = float.Parse(results[3]);
        }
    }

    private async Task<string> RunShellCommand(string command)
    {
        return await Task.Run(() =>
        {

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{command}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            return process.StandardOutput.ReadToEnd();
        });
    }

    /// <summary>
    /// Writes the statistics to a file.
    /// </summary>
    private async void WriteStats()
    {
        var stats = $"{RTT},{FPS},{CPU},{GPU},{Memory},{BatteryWatt},{Jitter}";
        using StreamWriter writer = new(filePath, true);
        await writer.WriteLineAsync(stats);
    }
}
