
using UnityEngine;

/// <summary>
/// Only sets the target frame-rate of the game to 30 FPS.
/// </summary>
public class FrameRateManager : MonoBehaviour
{
    /// <summary>
    /// A reference to an instance of this class. Used to implement the
    /// singleton pattern and prevent multiple instances of this class being
    /// created within the same game.
    /// </summary>
    public static FrameRateManager Instance { get; private set; }

    /// <summary>
    /// Sets the target framerate when the game boots up.
    /// </summary>
    /// <seealso cref="Application.targetFrameRate"/>
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
