using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider XAxisSlider;
    public Slider YAxisSlider;
    public Slider ZAxisSlider;

    private float scale = 180;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSlider()
    {
        Debug.Log("UpdateSlider()");
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.transform.localEulerAngles = new Vector3(XAxisSlider.value * scale, YAxisSlider.value * scale, ZAxisSlider.value * scale);
    }

}
