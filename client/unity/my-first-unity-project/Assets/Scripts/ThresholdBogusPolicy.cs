using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Diagnostics;

/// <summary>
/// A policy that toggles between client and thin client based on a 5 second
/// timer.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/ThresholdBogusPolicy")]
public class ThresholdBogusPolicy : Policy
{
    private readonly Stopwatch stopwatch = new();
    private bool isThinClient = false;

    public override PolicyResult Evaluate(PolicyData data)
    {
        if (!stopwatch.IsRunning)
            stopwatch.Start();

        if (stopwatch.ElapsedMilliseconds < 5000)
            return PolicyResult.None;

        isThinClient = !isThinClient;
        stopwatch.Reset();

        if (isThinClient)
            return PolicyResult.BecomeClient;
        else
            return PolicyResult.BecomeThinClient;
    }
}