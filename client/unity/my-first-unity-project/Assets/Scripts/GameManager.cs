using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

/// <summary>
/// This class will listen for remote commands that tell the game what to do.
/// Examples of such commands are logging in as a client to a specified server,
/// disconnecting, or switching from client to thin-client mode.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/GameManager")]
public class GameManager : ScriptableObject
{
    [SerializeField] private UserInputManager inputManager = default;

    public uint PlayerID { get; set; }
    public IPEndPoint ServerEndpoint { get; set; }

    void OnEnable()
    {
        // Register a callback for user input to switch scenes
        inputManager.ToggleThinClientEvent += ToggleThinClient;

        PlayerID = 0;
        ServerEndpoint = null;
    }

    void OnDisable()
    {
        inputManager.ToggleThinClientEvent -= ToggleThinClient;
    }

    void OnDestroy()
    {

    }

    public IEnumerator SwitchSceneRoutine(GameScenes scene)
    {
        var op = SwitchToScene(scene);
        while (op != null && !op.isDone)
        {
            yield return null;
        }
    }

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

    private void ToggleThinClient()
    {
        Debug.Log("toggling client");
        var currentSceneName = SceneManager.GetActiveScene().name;
        Enum.TryParse(currentSceneName, out GameScenes scene);
        var sceneToLoad = scene == GameScenes.Client ? GameScenes.ThinClient : GameScenes.Client;
        SceneManager.LoadScene(sceneToLoad.ToString());
    }

    // IEnumerator LoadYourAsyncScene()
    // {
    //     // The Application loads the Scene in the background as the current Scene runs.
    //     // This is particularly good for creating loading screens.
    //     // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
    //     // a sceneBuildIndex of 1 as shown in Build Settings.

    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Receiver");

    //     // Wait until the asynchronous scene fully loads
    //     while (!asyncLoad.isDone)
    //     {
    //         yield return null;
    //     }
    // }

    // Update is called once per frame
    void Update()
    {

    }
}
