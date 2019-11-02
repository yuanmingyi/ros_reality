using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightBox : MonoBehaviour
{
    public float angularSpeed = 180f;
    public float velocity = 1f;
    public float distance = 0.5f;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null)
        {
            transform.Rotate(0, Time.deltaTime * angularSpeed, 0, Space.Self);
            var targetPosition = mainCamera.transform.position + (transform.position - mainCamera.transform.position) * distance;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * velocity);
        }
    }
}
