using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    //跟随的物体
    public GameObject follow;
    //相机与物体相对偏移位置
    Vector3 offset;                  

    void Start()
    {
        offset = transform.position - follow.transform.position;
    }

    void FixedUpdate()
    {
        //摄像机自身位置到目标位置平滑过渡
        transform.position = Vector3.Lerp(transform.position, follow.transform.position + offset, 3 * Time.deltaTime);
    }
}
