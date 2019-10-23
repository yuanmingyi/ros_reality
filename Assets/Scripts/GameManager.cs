using RosSharp.RosBridgeClient;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;
using static Assets.RosSharp.Scripts.RosBridgeClient.RosCommuncation.Utilities;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public KinectScene kinectScene;
    public GameObject menu;
    public List<SteamVR_LaserPointer> pointers;
    public RosConnector rosConnector;
    public WandStateType wandStateType;

    private IEnumerable<PoseStampedPublisher> _poseStampedPublishers;
    private IEnumerable<IMessagePublisher> _wandStatePublishers;
    private bool _isMenuMode = true;
    private BoxSelectionManager _selectionManager;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var pointer in pointers)
        {
            pointer.PointerClick += OnPointClick;
            pointer.PointerIn += OnPointIn;
            pointer.PointerOut += OnPointOut;
        }
        _selectionManager = menu.GetComponentInChildren<BoxSelectionManager>();
        _poseStampedPublishers = rosConnector.GetComponents<PoseStampedPublisher>();
        _wandStatePublishers = rosConnector.GetComponents<IMessagePublisher>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions._default.Menu.GetStateUp(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Menu!");
            _isMenuMode = !_isMenuMode;
            menu.SetActive(_isMenuMode);
            kinectScene.gameObject.SetActive(!_isMenuMode);
            EnablePublishers(!_isMenuMode);
            EnablePointers(_isMenuMode);
        }

        if (!_isMenuMode)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                Debug.Log("Return key is Pressed Down");
                kinectScene.isFollowing = !kinectScene.isFollowing;
            }
            if (SteamVR_Actions._default.Teleport.GetState(SteamVR_Input_Sources.Any))
            {
                Debug.Log("Teleport!");
                kinectScene.Reset();
            }
            else if (Input.GetKey(KeyCode.H))
            {
                Debug.Log("H key is Pressed Down");
                kinectScene.Reset();
            }
        }
    }

    void LateUpdate()
    {
        if (_isMenuMode)
        {
            menu.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 1.5f;
            menu.transform.rotation = mainCamera.transform.rotation;
        }
    }

    private void EnablePointers(bool enabled)
    {
        foreach (var pointer in pointers)
        {
            var laser = pointer.GetComponent<SteamVR_LaserPointer>();
            laser.thickness = enabled ? 0.001f : 0;
        }
    }

    private void EnablePublishers(bool enabled)
    {
        foreach (var publisher in _wandStatePublishers)
        {
            publisher.IsEnabled = enabled && wandStateType == publisher.MessageType;
        }
        foreach (var publisher in _poseStampedPublishers)
        {
            publisher.enabled = enabled;
        }
    }

    private void OnPointOut(object sender, PointerEventArgs e)
    {
        //Debug.Log($"OnPointOut, {e.target}");
        _selectionManager.HoverOnMenuEnd(e);
    }

    private void OnPointIn(object sender, PointerEventArgs e)
    {
        //Debug.Log($"OnPointIn, {e.target}");
        _selectionManager.HoverOnMenuStart(e);
    }

    private void OnPointClick(object sender, PointerEventArgs e)
    {
        //Debug.Log($"OnPointClick, {e.target}");
        _selectionManager.ClickOnMenu(e);
    }

}
