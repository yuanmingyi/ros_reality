using UnityEngine;
using System.Collections;
using System;
using RosSharp.RosBridgeClient;

public class KinectSceneRender : MonoBehaviour
{
    public Material material;
    public int imageWidth = 512;
    public int imageHeight = 424;
    public GameObject rosConnector;

    // Use this for initialization
    void Start() {
        Debug.Log("DepthRosGeometryView.Start()");
    }

    void OnRenderObject() {
        foreach (var subscriber in rosConnector.GetComponents<RawImageSubscriber>())
        {
            if (subscriber.enabled)
            {
                Debug.Log($"Add Raw Image Subscriber: {subscriber.texName}, {subscriber.Topic}");
                material.SetTexture(subscriber.texName, subscriber.Texture2D);
            }
        }
        foreach (var subscriber in rosConnector.GetComponents<ImageSubscriberV2>())
        {
            if (subscriber.enabled)
            {
                Debug.Log($"Add Image SubscriberV2: {subscriber.texName}, {subscriber.Topic}");
                material.SetTexture(subscriber.texName, subscriber.Texture2D);
            }
        }
        material.SetPass(0);
        Matrix4x4 mat = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        material.SetMatrix("transformationMatrix", mat);

        Graphics.DrawProceduralNow(MeshTopology.Points, imageWidth * imageHeight, 1);
    }
}