using UnityEngine;
using System.Collections;
using System;

public class DepthRosGeometryView : MonoBehaviour {

    //depthTopic = "kinect2/sd/image_depth_rect_throttle";
    //colorTopic = "kinect2/sd/image_color_rect/compressed_throttle";
    private const string DEPTH_TOPIC = "kinect2/sd/image_depth_rect";
    private const string COLOR_TOPIC = "kinect2/sd/image_color_rect/compressed";
    private const int _imageWidth = 512;
    private const int _imageHeight = 424;

    public WebsocketClient _wsc;
    public int Framerate = 100;
    public string Compression = "none"; //"png" is the other option, haven't tried it yet though
    public Material Material;

    private Texture2D _depthTexture;
    private Texture2D _colorTexture;

    private Matrix4x4 _m;

    public void CheckMessages()
    {
        if (_wsc != null)
        {
            Debug.Log("[Message Dictionary Start]:");
            foreach (var message in _wsc.messages)
            {
                Debug.Log($"key={message.Key}, value={message.Value}");
            }
            Debug.Log("[Message Dictionary End]");
        }
    }

    // Use this for initialization
    void Start() {
        Debug.Log("DepthRosGeometryView.Start()");

        // Create a texture for the depth image and color image
        _depthTexture = new Texture2D(_imageWidth, _imageHeight, TextureFormat.R16, false);
        _colorTexture = new Texture2D(2, 2);

        _wsc.Subscribe(DEPTH_TOPIC, "sensor_msgs/Image", Compression, Framerate);
        _wsc.Subscribe(COLOR_TOPIC, "sensor_msgs/CompressedImage", Compression, Framerate);
        StartCoroutine(UpdateKinectTexture());
    }

    IEnumerator UpdateKinectTexture()
    {
        while (true)
        {
            if (_wsc.messages.ContainsKey(DEPTH_TOPIC))
            {
                try
                {
                    var depthMessage = _wsc.messages[DEPTH_TOPIC];
                    byte[] depthImage = System.Convert.FromBase64String(depthMessage);
                    _depthTexture.LoadRawTextureData(depthImage);
                    _depthTexture.Apply();
                }
                catch (Exception e)
                {
                    Debug.Log($"Error: {DEPTH_TOPIC}: {e.ToString()}");
                }
            }
            if (_wsc.messages.ContainsKey(COLOR_TOPIC))
            {
                try
                {
                    var colorMessage = _wsc.messages[COLOR_TOPIC];
                    byte[] colorImage = System.Convert.FromBase64String(colorMessage);
                    _colorTexture.LoadImage(colorImage);
                    _colorTexture.Apply();
                }
                catch (Exception e)
                {
                    Debug.Log($"Error: {COLOR_TOPIC}: {e.ToString()}");
                }
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    void OnRenderObject() {
        Material.SetTexture("_MainTex", _depthTexture);
        Material.SetTexture("_ColorTex", _colorTexture);
        Material.SetPass(0);

        _m = Matrix4x4.TRS(this.transform.position, this.transform.rotation, this.transform.localScale);
        Material.SetMatrix("transformationMatrix", _m);

        Graphics.DrawProceduralNow(MeshTopology.Points, _imageWidth * _imageHeight, 1);
    }
}