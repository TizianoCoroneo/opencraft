using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// A policy that evaluates to no result.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/ThresholdNoPolicy")]
public class ThresholdNoPolicy : Policy
{
    public override PolicyResult Evaluate(PolicyData data)
    {
        return PolicyResult.None;
    }
}