using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class KinectScene : MonoBehaviour
{
    public GameObject FollowingObject;
    public bool StickToCamera;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bool reset = false;
        //if (SteamVR_Actions._default.GrabPinch.GetState(SteamVR_Input_Sources.Any))
        //{
        //    // send pose message to the ros
        //    var location = SteamVR_Actions._default.Pose.localPosition;
        //    var rotation = SteamVR_Actions._default.Pose.localRotation;
        //    Debug.Log($"location: {location}, rotation: {rotation}");
        //}
        if (SteamVR_Actions._default.Teleport.GetState(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Teleport!");
            reset = true;
        }
        else if (Input.GetKey(KeyCode.H))
        {
            Debug.Log("H key is Pressed Down");
            reset = true;
        }
        else if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log("Return key is Pressed Down");
            StickToCamera = !StickToCamera;
        }
        if (reset || StickToCamera)
        {
            ResetToFollowingObject();
        }
    }

    private void ResetToFollowingObject()
    {
        transform.position = FollowingObject.transform.position;
        transform.rotation = FollowingObject.transform.rotation;
    }
}
