using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PolicyManager", menuName = "ScriptableObjects/PolicyManager")]
public class PolicyManager : ScriptableObject
{
    [SerializeField] public Policy Policy;
}
