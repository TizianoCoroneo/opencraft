using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Unity.RenderStreaming.Samples;

public class Experiment
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ExperimentWithEnumeratorPasses()
    {
        SceneManager.LoadScene("Scenes/Receiver/SceneThinClient");
        Debug.Log("Scene loaded");

        yield return new WaitForSeconds(1);

        var receiver = GameObject.Find("ReceiverSample");
        var component = receiver.GetComponent<ReceiverSample>();
        
        component.OnStart();
        
        while (true)
        {
            yield return new WaitForSeconds(1);
        }
    }
}
