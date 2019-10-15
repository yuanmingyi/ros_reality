using UnityEngine;
using System.Collections;
using System;
using RosSharp.RosBridgeClient;
using System.Collections.Generic;

public class KinectSceneRender : MonoBehaviour
{
    public Material material;
    public int imageWidth = 512;
    public int imageHeight = 424;
    public GameObject rosConnector;

    private List<IImageSubscriber> subscribers;

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

    void OnRenderObject()
    {
        foreach (var subscriber in subscribers)
        {
            material.SetTexture(subscriber.TexName, subscriber.Texture2D);
        }
        material.SetPass(0);
        Matrix4x4 mat = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        material.SetMatrix("transformationMatrix", mat);
        Graphics.DrawProceduralNow(MeshTopology.Points, imageWidth * imageHeight, 1);
    }
}