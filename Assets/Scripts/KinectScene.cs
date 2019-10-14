using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        //if (SteamVR_Input..GetActionSet("buggy")
        //    Debug.Log("RightHand trigger is Pressed Down");
        //    reset = true;
        //}
        //else if (ViveInput.GetPressDownEx(HandRole.LeftHand, ControllerButton.Trigger))
        //{
        //    Debug.Log("LeftHand trigger is Pressed Down");
        //    reset = true;
        //}
        if (Input.GetKey(KeyCode.H))
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
