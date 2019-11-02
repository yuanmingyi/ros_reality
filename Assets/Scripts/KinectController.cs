using Assets.Scripts;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.MessageTypes.RosSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;

public class KinectController : WorldController
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public override void HandleInput()
    {
        if (SteamVR_Actions._default.Menu.GetStateDown(inputSource))
        {
            sceneSwitch.GotoSelectionWorld();
        }
        else if (SteamVR_Actions._default.Teleport.GetStateDown(inputSource))
        {
            sceneSwitch.ResetKinectWorld();
        }
    }
}
