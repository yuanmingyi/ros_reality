using RosSharp.RosBridgeClient;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(RosConnector))]
public class PosePublisherTrigger : MonoBehaviour
{
    public string leftPoseTopic = "/left_hand_pose";
    public string rightPoseTopic = "/right_hand_pose";

    private PoseStampedPublisher _leftPublisher;
    private PoseStampedPublisher _rightPublisher;
    private RosConnector _connector;
    private bool _isAdvertised = false;
    private readonly int _secondsTimeout = 1;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var publisher in GetComponents<PoseStampedPublisher>())
        {
            if (publisher.Topic.Equals(leftPoseTopic))
            {
                _leftPublisher = publisher;
            }
            else if (publisher.Topic.Equals(rightPoseTopic))
            {
                _rightPublisher = publisher;
            }
        }
        _connector = GetComponent<RosConnector>();
        new Thread(AdvertiseTopic).Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAdvertised)
        {
            _leftPublisher.enabled = SteamVR_Actions._default.GrabPinch.GetState(SteamVR_Input_Sources.LeftHand);
            _rightPublisher.enabled = SteamVR_Actions._default.GrabPinch.GetState(SteamVR_Input_Sources.RightHand);
        }
    }

    private void AdvertiseTopic()
    {
        if (!_connector.IsConnected.WaitOne(_secondsTimeout * 1000))
        {
            Debug.LogWarning("Failed to advertise: RosConnector not connected");
            _isAdvertised = false;
        }
        else
        {
            _connector.RosSocket.Advertise<RosSharp.RosBridgeClient.MessageTypes.Geometry.PoseStamped>(leftPoseTopic);
            _connector.RosSocket.Advertise<RosSharp.RosBridgeClient.MessageTypes.Geometry.PoseStamped>(rightPoseTopic);
            _isAdvertised = true;
        }
    }
}
