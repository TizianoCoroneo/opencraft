using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Diagnostics;

[CreateAssetMenu(menuName = "ScriptableObjects/ThresholdBogusPolicy")]
public class ThresholdBogusPolicy : Policy
{
    private Stopwatch stopwatch = new Stopwatch();
    private bool isThinClient = false;

    public override Policy.PolicyResult Evaluate(Policy.PolicyData data)
    {
        if (!stopwatch.IsRunning)
            stopwatch.Start();

        if (stopwatch.ElapsedMilliseconds < 5000)
            return Policy.PolicyResult.None;

        isThinClient = !isThinClient;
        stopwatch.Reset();

        if (isThinClient)
            return Policy.PolicyResult.BecomeClient;
        else
            return Policy.PolicyResult.BecomeThinClient;
    }
}