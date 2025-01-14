using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// A policy that evaluates the round-trip time (RTT) and determines the appropriate client type.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Threshold Combo Policy")]

public class ThresholdComboPolicy : Policy
{
    public override PolicyResult Evaluate(PolicyData data)
    {
        UnityEngine.Debug.Log($"TEST {data.stats.RTT}");
        if (data.stats.RTT > 100 || data.stats.FPS > 25 || data.stats.CPU < 20 || data.stats.GPU < 20 || data.stats.Memory < 20 || data.stats.BatteryWatt < 20)
            return PolicyResult.BecomeClient;
        else if (data.stats.RTT < 85 || data.stats.FPS < 20 || data.stats.CPU > 80 || data.stats.GPU > 80 || data.stats.Memory > 80 || data.stats.BatteryWatt > 35)
            return PolicyResult.BecomeThinClient;
        else
            return PolicyResult.None;
    }
}