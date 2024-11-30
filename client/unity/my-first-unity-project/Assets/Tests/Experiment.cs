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
        SceneManager.LoadScene("Scenes/Receiver/SceneThinClient");
        Debug.Log("Scene loaded");

        yield return new WaitForSeconds(1);
        
        // var receiver = GameObject.Find("ReceiverSample");
        // var component = receiver.GetComponent<ReceiverSample>();
        //
        // component.OnStart();
        
        var keyboard = InputSystem.AddDevice<Keyboard>();
        // var mouse = Mouse.current;
        // Move(mouse.delta, new Vector2(100, 100));
        
        Press(keyboard.wKey);
        
        yield return new WaitForSeconds(60);

        yield return KeepFlipping();
    }

    IEnumerator KeepFlipping()
    {
        var isThinClient = false;

        for (int i = 0; i < 10; i++)
        { 
            yield return new WaitForSeconds(10);

            if (isThinClient) yield return BecomeThinClient();
            else yield return BecomeClient();

            isThinClient = !isThinClient;
        }        
    }

    IEnumerator BecomeThinClient() {
        using var www = UnityWebRequest.Get("http://localhost:7980/become/thinclient?host=localhost&port=7999&signalingPort=7981");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) Debug.Log(www.error);
        else Debug.Log("Became thin client!");
    }
    
    IEnumerator BecomeClient() {
        using var www = UnityWebRequest.Get("http://localhost:7980/become/client?host=localhost&port=7979&playerID=1");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) Debug.Log(www.error);
        else Debug.Log("Became client!");
    }
}
