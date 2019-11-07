using UnityEngine;
using System.Collections;
using System;
using RosSharp.RosBridgeClient;
using System.Collections.Generic;
using UnityEngine.UI;

public class KinectScene : MonoBehaviour
{
    public Material material;
    public int imageWidth = 512;
    public int imageHeight = 424;
    public GameObject rosConnector;
    public Image kinectColor;

    //public bool isFollowing;
    //public Transform followingTarget;

    private List<IImageSubscriber> subscribers;

    public void Reset()
    {
        //transform.rotation = followingTarget.transform.rotation;
        //transform.position = followingTarget.transform.position;
    }

    // Use this for initialization
    void Start() {
        subscribers = new List<IImageSubscriber>();
        foreach (var subscriber in rosConnector.GetComponents<RawImageSubscriber>())
        {
            if (subscriber.enabled)
            {
                subscribers.Add(subscriber);
            }
        }
        foreach (var subscriber in rosConnector.GetComponents<ImageSubscriberV2>())
        {
            if (subscriber.enabled)
            {
                subscribers.Add(subscriber);
            }
        }
    }

    //void LateUpdate()
    //{
    //    if (isFollowing)
    //    {
    //        Reset();
    //    }
    //}

    void OnRenderObject()
    {
        foreach (var subscriber in subscribers)
        {
            material.SetTexture(subscriber.TexName, subscriber.Texture2D);
            if (subscriber.TexName.Contains("Color") && kinectColor != null)
            {
                kinectColor.sprite = Sprite.Create(subscriber.Texture2D, new Rect(0, 0, imageWidth, imageHeight), Vector2.one / 2);
            }
        }
        material.SetPass(0);
        Matrix4x4 mat = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        material.SetMatrix("transformationMatrix", mat);
        Graphics.DrawProceduralNow(MeshTopology.Points, imageWidth * imageHeight, 1);
    }
}