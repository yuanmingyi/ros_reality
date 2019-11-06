using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SceneSwitch : MonoBehaviour
{
    public WorldController prefaceWorld;
    public WorldController kinectWorld;
    public WorldController selectionWorld;
    public Transform environment;
    public Transform cameraTrans;

    private Vector3 _originalKinectWorldPos;
    private Quaternion _originalKinectWorldRotate;
    private Vector3 _originalSelectionPos;
    private Quaternion _originalSelectionRotate;
    private Vector3 _initialReferringPos;
    private Quaternion _initialReferringRotate;

    // Start is called before the first frame update
    void Start()
    {
        _originalSelectionPos = selectionWorld.transform.position;
        _originalSelectionRotate = selectionWorld.transform.rotation;
        _originalKinectWorldPos = kinectWorld.transform.position;
        _originalKinectWorldRotate = kinectWorld.transform.rotation;
        _initialReferringPos = Vector3.up * 1.4f;
        _initialReferringRotate = Quaternion.identity;
    }

    public void CalibrateInitialTransform()
    {
        _initialReferringPos = cameraTrans.position;
        _initialReferringRotate = cameraTrans.rotation;
    }

    public void GotoKinectWorld()
    {
        Debug.Log("Goto kinect world");
        ResetKinectWorld(_initialReferringPos, _initialReferringRotate);
        prefaceWorld.OnUnload();
        selectionWorld.OnUnload();
        ClearHints();
        kinectWorld.OnLoad();
    }

    public void GotoPrefaceWorld()
    {
        Debug.Log("Goto preface world");
        ResetPrefaceWorld(_initialReferringPos, _initialReferringRotate);
        selectionWorld.OnUnload();
        kinectWorld.OnUnload();
        ClearHints();
        prefaceWorld.OnLoad();
    }

    public void GotoSelectionWorld()
    {
        Debug.Log("Goto selection world");
        ResetSelectionWorld(_initialReferringPos, _initialReferringRotate);
        prefaceWorld.OnUnload();
        kinectWorld.OnUnload();
        ClearHints();
        selectionWorld.OnLoad();
    }

    public void ResetSelectionWorld()
    {
        ResetSelectionWorld(cameraTrans.position, cameraTrans.rotation);
        ResetSelectionWorld(_initialReferringPos, _initialReferringRotate);
    }

    public void ResetKinectWorld()
    {
        ResetKinectWorld(_initialReferringPos, _initialReferringRotate);
    }

    public void ResetPrefaceWorld()
    {
        ResetPrefaceWorld(_initialReferringPos, _initialReferringRotate);
    }

    private void ResetSelectionWorld(Vector3 referringPos, Quaternion referringRotate)
    {
        prefaceWorld.transform.position = referringPos - _originalSelectionPos;
        prefaceWorld.transform.rotation = referringRotate * Quaternion.Inverse(_originalSelectionRotate);
        kinectWorld.transform.position = prefaceWorld.transform.position + _originalKinectWorldPos;
        kinectWorld.transform.rotation = prefaceWorld.transform.rotation * _originalKinectWorldRotate;
        environment.position = referringPos;
        environment.rotation = referringRotate;
        selectionWorld.transform.position = referringPos;
        selectionWorld.transform.rotation = referringRotate;
    }

    private void ResetKinectWorld(Vector3 referringPos, Quaternion referringRotate)
    {
        prefaceWorld.transform.position = referringPos - _originalKinectWorldPos;
        prefaceWorld.transform.rotation = referringRotate * Quaternion.Inverse(_originalKinectWorldRotate);
        selectionWorld.transform.position = prefaceWorld.transform.position + _originalSelectionPos;
        selectionWorld.transform.rotation = prefaceWorld.transform.rotation * _originalSelectionRotate;
        environment.position = referringPos;
        environment.rotation = referringRotate;
        kinectWorld.transform.position = referringPos;
        kinectWorld.transform.rotation = referringRotate;
    }

    private void ResetPrefaceWorld(Vector3 referringPos, Quaternion referringRotate)
    {
        selectionWorld.transform.position = referringPos + _originalSelectionPos;
        selectionWorld.transform.rotation = referringRotate * _originalSelectionRotate;
        kinectWorld.transform.position = referringPos + _originalKinectWorldPos;
        kinectWorld.transform.rotation = referringRotate * _originalKinectWorldRotate;
        environment.position = referringPos;
        environment.rotation = referringRotate; 
        prefaceWorld.transform.position = referringPos;
        prefaceWorld.transform.rotation = referringRotate;
    }

    private void ClearHints()
    {
        ControllerHintsManager.HideAllHints(Player.instance.leftHand);
        ControllerHintsManager.HideAllHints(Player.instance.rightHand);
    }

}
