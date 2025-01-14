using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Unity.RenderStreaming.Samples;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class Experiment : InputTestFixture
{
    [UnityTest]
    public IEnumerator GoForward60Seconds()
    {
        SceneManager.LoadScene("Scenes/Client/Client");
        Debug.Log("Scene loaded");

        yield return new WaitForSeconds(1);

        var keyboard = InputSystem.AddDevice<Keyboard>();
        Press(keyboard.wKey);

        yield return new WaitForSeconds(61);
    }
}
