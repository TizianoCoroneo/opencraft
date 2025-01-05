

using UnityEngine;

public abstract class Policy : ScriptableObject
{
    public abstract PolicyResult Evaluate(PolicyData data);

    public enum PolicyResult
    {
        None,
        BecomeClient,
        BecomeThinClient
    }

    public class PolicyData
    {
        public Statistics stats;
    }
}