using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// A policy that evaluates the round-trip time (RTT) and determines the appropriate client type.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Threshold System Policy")]

public class ThresholdSystemPolicy : Policy
{
    public override PolicyResult Evaluate(PolicyData data)
    {
        if (data.stats.FPS > 25 || data.stats.GPU < 40 || data.stats.BatteryWatt < 25)
            return PolicyResult.BecomeClient;
        else if (data.stats.FPS < 20 || data.stats.GPU > 85 || data.stats.BatteryWatt > 35)
            return PolicyResult.BecomeThinClient;
        else
            return PolicyResult.None;
    }
}