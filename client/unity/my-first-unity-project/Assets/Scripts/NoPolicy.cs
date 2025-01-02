using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "ScriptableObjects/NoPolicy")]
public class NoPolicy : Policy
{
    public override Policy.PolicyResult Evaluate(Policy.PolicyData data)
    {
       return Policy.PolicyResult.None;
    }
}