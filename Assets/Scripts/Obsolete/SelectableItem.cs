using System;
using System.Collections;
using System.Collections.Generic;
using RosSharp.RosBridgeClient.MessageTypes.RosSharp;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class SelectableItem: MonoBehaviour
{
    public string id;

    private bool _hasBox;
    public bool HasBox
    {
        get { return _hasBox; }
        set
        {
            _hasBox = value;
            BoxImage.enabled = _hasBox;
        }
    }

    public Image BoxImage
    {
        get;
        private set;
    }

    private Image _buttonImage;

    // Start is called before the first frame update
    void Awake()
    {
        BoxImage = transform.Find("BoxImage").GetComponent<Image>();
        _buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Highlight(bool highlight)
    {
        float alpha = highlight ? 0.5f : 0;
        _buttonImage.color = new Color(0, 0, 0, alpha);
    }

}
