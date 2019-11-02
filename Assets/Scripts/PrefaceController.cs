using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PrefaceController: WorldController
{
    public Text prefaceText;
    public List<string> prefaceWords;

    List<string>.Enumerator _enumerator;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void Reset()
    {
        _enumerator = prefaceWords.GetEnumerator();
        if (_enumerator.MoveNext())
        {
            prefaceText.text = _enumerator.Current;
        }
    }

    public override void OnLoad()
    {
        base.OnLoad();
        Reset();
    }

    // Update is called once per frame
    public override void HandleInput()
    {
        if (SteamVR_Actions.default_GrabGrip.GetStateDown(inputSource))
        {
            Debug.Log("Go to the next tip");
            if (_enumerator.MoveNext())
            {
                prefaceText.text = _enumerator.Current;
            }
            else
            {
                CompleteStage();
            }
        }
        else if (SteamVR_Actions.default_Menu.GetStateDown(inputSource))
        {
            Debug.Log("Skip the tutorial");
            CompleteStage();
        }
        else if (SteamVR_Actions.default_Teleport.GetStateDown(inputSource))
        {
            Debug.Log("reset the view");
            sceneSwitch.ResetPrefaceWorld();
        }
        else if (SteamVR_Actions.default_GrabPinch.GetStateDown(inputSource))
        {
            Debug.Log("reset the preface");
            Reset();
        }
    }

    private void CompleteStage()
    {
        Debug.Log("Complete Stage Preface");
        //SceneManager.LoadScene(1);
        sceneSwitch.GotoKinectWorld();
    }

}
