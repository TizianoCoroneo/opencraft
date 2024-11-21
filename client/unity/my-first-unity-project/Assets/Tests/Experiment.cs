using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Unity.RenderStreaming.Samples;
using UnityEngine.InputSystem;

public class Experiment : InputTestFixture
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
        
        yield return new WaitForSeconds(1);

        var keyboard = InputSystem.AddDevice<Keyboard>();
        var mouse = Mouse.current;
        
        Move(mouse.delta, new Vector2(100, 100));
        
        while (true)
        {
            Press(keyboard.wKey);
        
            yield return new WaitForSeconds(5);
        
            Release(keyboard.wKey);
            
            Move(mouse.delta, new Vector2(100, 100));
            
            yield return new WaitForSeconds(1);
        }
    }
}
