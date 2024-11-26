using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

/// <summary>
/// This class can switch the client's deployment.
/// Examples include logging in as a client to a specified server,
/// disconnecting, or switching from client to thin-client mode.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/GameManager")]
public class GameManager : ScriptableObject
{
    [SerializeField] private UserInputManager inputManager = default;

    /// <summary>
    /// The player this client represents.
    /// </summary>
    public uint PlayerID { get; set; }

    /// <summary>
    /// The server endpoint, IP and port.
    /// </summary>
    public IPEndPoint ServerEndpoint { get; set; }

    /// <summary>
    /// Called when this script is enabled.
    /// 
    /// <para>
    /// Registers an input callback to
    /// toggle between client and thin client mode, allowing the user to switch
    /// between these two modes by pressing a button. This can be helpful when
    /// testing or debugging.
    /// </para>
    /// </summary>
    void OnEnable()
    {
        // Register a callback for user input to switch scenes
        inputManager.ToggleThinClientEvent += ToggleThinClient;

        PlayerID = 0;
        ServerEndpoint = null;
    }

    /// <summary>
    /// Called when this script is disabled. Removes registered callbacks.
    /// </summary>
    void OnDisable()
    {
        inputManager.ToggleThinClientEvent -= ToggleThinClient;
    }

    void OnDestroy()
    {

    }

    /// <summary>
    /// Switches the client to the given scene. This method can be run as a
    /// Unity Coroutine so that the switching can take place across multiple
    /// frames without freezing the main thread.
    /// </summary>
    /// <param name="scene">The scene to switch to.</param>
    /// <returns>An enumerator used by Unity's Coroutine implementation to check
    /// if the operation has completed.</returns>
    public IEnumerator SwitchSceneRoutine(GameScenes scene)
    {
        var op = SwitchToScene(scene);
        while (op != null && !op.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Switch the client to the given scene using an AsyncOperation, which you
    /// can check for completion. If you want to switch scenes in a Unity
    /// coroutine, you probably want to use the <see cref="SwitchSceneRoutine"/>
    /// method instead.
    /// </summary>
    /// <param name="scene">The scene to switch to.</param>
    /// <returns>An AsyncOperation which you can use to check if the switch has
    /// completed.</returns>
    public AsyncOperation SwitchToScene(GameScenes scene)
    {
        string sceneName = default;
        switch (scene)
        {
            case GameScenes.Client:
                sceneName = "Client";
                break;
            case GameScenes.ThinClient:
                sceneName = "ThinClient";
                break;
            default:
                Assert.IsTrue(false);
                break;
        }
        var currSceneName = SceneManager.GetActiveScene().name;
        if (currSceneName != sceneName)
        {
            return SceneManager.LoadSceneAsync(sceneName);
        }
        return null;
    }

    /// <summary>
    /// Toggles between a client and thin client, used for debugging/testing.
    /// </summary>
    private void ToggleThinClient()
    {
        Debug.Log("toggling client");
        var currentSceneName = SceneManager.GetActiveScene().name;
        Enum.TryParse(currentSceneName, out GameScenes scene);
        var sceneToLoad = scene == GameScenes.Client ? GameScenes.ThinClient : GameScenes.Client;
        SceneManager.LoadScene(sceneToLoad.ToString());
    }

    void Update()
    {

    }
}
