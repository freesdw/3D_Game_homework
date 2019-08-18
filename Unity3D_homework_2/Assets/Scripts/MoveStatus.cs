using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//对象移动处理
public class MoveStatus : MonoBehaviour {

    float speed = 15;//移动的速度
    //用来检测移动的状态。由于上船或下船的时候，有两个移动段。
    //0代表在开始位置，1代表从开始位置移到转折点，2代表从转折点移动到目的地
    public int move_status;
    //目的地的地址
    Vector3 destination;
    //转折点的地址
    Vector3 halfDest;

    private void Update()
    {
        //从开始移动到转折点，到转折点后，改变状态
        if(move_status == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, halfDest, speed * Time.deltaTime);
            if(transform.position == halfDest)
            {
                move_status = 2;
            }
        }
        //从转折点到目的地，到目的地后，改变状态
        else if(move_status == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if(transform.position == destination)
            {
                move_status = 0;
            }
        }
    }
    //重置移动状态
    public void Reset()
    {
        move_status = 0;
    }
    //设置目的地
    public void setDestination(Vector3 dest)
    {
        destination = dest;
        move_status = 1;
        //如果高度一样，证明不用经过转折点，就是，在水面上船的移动
        if (dest.y == transform.position.y)
        {
            move_status = 2;
        }
        //上岸时转折处理
        else if(dest.y > transform.position.y)
        {
            halfDest.x = transform.position.x;
            halfDest.y = dest.y;

        }
        //上船时转折处理
        else
        {
            halfDest.y = transform.position.y;
            halfDest.x = dest.x;
        }
        
    } 
}
