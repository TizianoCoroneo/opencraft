using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// A policy that evaluates the round-trip time (RTT) and determines the appropriate client type.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/ThresholdRTTPolicy")]

public class ThresholdRTTPolicy : Policy
{
    public override PolicyResult Evaluate(PolicyData data)
    {
        if (data.stats.RTT > 80)
            return PolicyResult.BecomeClient;
        else if (data.stats.RTT < 60)
            return PolicyResult.BecomeThinClient;
        else
            return PolicyResult.None;
    }
}