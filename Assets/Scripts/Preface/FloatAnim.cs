using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAnim : MonoBehaviour
{
    float radian = 0; // 弧度
    float perRadian = 0.03f; // 每次变化的弧度
    float radius = 0.2f; // 半径
    Vector3 initialPosition;
    float maxRotation = 10.0f;
    float minInterval = 0.5f;
    float interval = 0f;

    // Use this for initialization
    void Start()
    {
        initialPosition = transform.localPosition; // 将最初的位置保存到oldPos
        Random.InitState((int)(Time.time * 1000));
    }

    // Update is called once per frame
    void Update()
    {
        // update position
        radian += perRadian; // 弧度每次加0.03
        float dy = Mathf.Cos(radian) * radius; // dy定义的是针对y轴的变量，也可以使用sin，找到一个适合的值就可以
        transform.localPosition = initialPosition + new Vector3(0, dy, 0);

        // update rotation
        interval += Time.deltaTime;
        float ry = interval < minInterval ? 0f : maxRotation * Random.value;
        if (interval > minInterval)
        {
            interval = 0f;
            transform.localEulerAngles = Vector3.up * ry;
        }
    }
}
