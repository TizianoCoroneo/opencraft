using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "ScriptableObjects/ThresholdRTTPolicy")]
public class ThresholdRTTPolicy : Policy
{
    public override Policy.PolicyResult Evaluate(Policy.PolicyData data) {
        if (data.CurrentRTT > 80) {
            return Policy.PolicyResult.BecomeClient;
        } else if (data.CurrentRTT < 60) {
            return Policy.PolicyResult.BecomeThinClient;
        } else {
            return Policy.PolicyResult.None;   
        }
    }    
}