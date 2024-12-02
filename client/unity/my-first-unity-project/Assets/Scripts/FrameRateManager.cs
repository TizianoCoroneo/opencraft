
using UnityEngine;

/// <summary>
/// Only sets the target frame-rate of the game to 30 FPS.
/// </summary>
public class FrameRateManager : MonoBehaviour
{
    public static FrameRateManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 30;
    }
}
