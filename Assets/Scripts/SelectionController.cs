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

public class SelectionController : WorldController
{
    public List<SteamVR_LaserPointer> pointers;
    public RosConnector rosConnector;
    public float selectedDelay = 1.0f;

    private ImageButton[] buttons;
    private bool _hasSelected;

    // Start is called before the first frame update
    void Start()
    {
        buttons = FindObjectsOfType<ImageButton>();
        foreach (var pointer in pointers)
        {
            pointer.PointerClick += OnPointClick;
            pointer.PointerIn += OnPointIn;
            pointer.PointerOut += OnPointOut;
        }
    }

    public override void OnLoad()
    {
        base.OnLoad();
    }

    public override void OnUnload()
    {
        base.OnUnload();
    }

    public override void Reset()
    {
        Debug.Log("Reset the selections");
        foreach (var button in buttons)
        {
            button.OnUnselect();
        }
        _hasSelected = false;
    }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

        if (SteamVR_Actions._default.Menu.GetStateDown(inputSource))
        {
            sceneSwitch.GotoKinectWorld();
        }
        else if (SteamVR_Actions._default.Teleport.GetStateDown(inputSource))
        {
            sceneSwitch.ResetSelectionWorld();
        }
    }

    private void OnPointOut(object sender, PointerEventArgs e)
    {
        //Debug.Log($"OnPointOut, {e.target}");
        var imageButton = GetImageButton(e.target.parent);
        if (imageButton != null)
        {
            imageButton.OnNormal();
        }
    }

    private void OnPointIn(object sender, PointerEventArgs e)
    {
        //Debug.Log($"OnPointIn, {e.target}");
        var imageButton = GetImageButton(e.target.parent);
        if (imageButton != null)
        {
            imageButton.OnHover();
        }
    }

    private void OnPointClick(object sender, PointerEventArgs e)
    {
        //Debug.Log($"OnPointClick, {e.target}");
        var imageButton = GetImageButton(e.target.parent);
        if (!_hasSelected && imageButton != null)
        {
            imageButton.OnSelect();
            SendGripMessage(imageButton.id);
            _hasSelected = true;
            StartCoroutine(SwitchScene());
        }
    }

    private IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(selectedDelay);
        sceneSwitch.GotoKinectWorld();
    }

    private ImageButton GetImageButton(Transform obj)
    {
        if (obj != null)
        {
            return obj.GetComponent<ImageButton>();
        }
        return null;
    }

    private void SendGripMessage(string id)
    {
        Debug.Log($"SendGripMessage: {id}");
        rosConnector.RosSocket.CallService<PickGiftRequest, PickGiftResponse>("/ros_sharp/PickGift", new ServiceResponseHandler<PickGiftResponse>((response) =>
        {
        }), new PickGiftRequest(id));
    }

}
