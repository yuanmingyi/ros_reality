using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GlobalController : MonoBehaviour
{
    private WorldController[] controllers;

    // Start is called before the first frame update
    void Start()
    {
        controllers = FindObjectsOfType<WorldController>();
        var sceneSwitch = GetComponent<SceneSwitch>();
        sceneSwitch.GotoPrefaceWorld();
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        var sceneSwitch = GetComponent<SceneSwitch>();
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Calibrate the scene position and rotation");
            sceneSwitch.CalibrateInitialTransform();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Goto Kinect world");
            sceneSwitch.GotoKinectWorld();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Goto Selection world");
            sceneSwitch.GotoSelectionWorld();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Goto Preface world");
            sceneSwitch.GotoPrefaceWorld();
        }
        else if (SteamVR_Actions.default_HeadsetOnHead.GetStateDown(SteamVR_Input_Sources.Head))
        {
            Debug.Log("head on. Start the tutorial");
            foreach (var controller in controllers)
            {
                controller.Reset();
            }
            sceneSwitch.GotoPrefaceWorld();
        }

        var activeStates = new List<bool>();
        foreach (var controller in controllers)
        {
            activeStates.Add(controller.IsLoaded);
        }
        for(int i = 0; i < activeStates.Count; i++)
        {
            if (activeStates[i])
            {
                ControllerHintsManager.ShowHints(controllers[i].Hints);
                controllers[i].HandleInput();
            }
        }
    }
}
