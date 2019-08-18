using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//物理引擎投掷动作，很多实现同运动学
public class Emit : SSAction
{
    //标记是否可以扔
    bool enableEmit = true;
    //扔飞碟的力的方向
    Vector3 force; 
    //随机开始位置
    float startX; 
    public SceneController sceneController = (SceneController)SSDirector.getInstance().currentScenceController; 

    public override void Start()
    {
        //生成随机位置
        startX = 6+Random.value * 12;
        this.transform.position = new Vector3(startX, 0, 0);
        //根据关数生成投掷的力气及方向
        force = new Vector3(6 * Random.Range(-1, 1) + sceneController.round*2, 6 * Random.Range(0.5f, 2), 10 + 5 * sceneController.round);
        //增加重力
        gameobject.GetComponent<Rigidbody>().useGravity = true;
    }
    //获取投掷动作
    public static Emit GetSSAction()
    {
        Emit action = ScriptableObject.CreateInstance<Emit>();
        return action;
    }
    //用于监控飞盘是否已失效，并设置参数方便回收
    public override void Update()
    {
        if (this.gameobject.transform.position.y < -12)
        {
            enableEmit = false;
            enable = false;
            destroy = true;
            this.callback.SSActionEvent(this);
            sceneController.addLoss();
        }
    }
    //销毁动作
    public void Destory()
    {
        this.destroy = true;
        this.callback.SSActionEvent(this);
    }
    //飞行过程
    public override void FixedUpdate()
    {
        if (!this.destroy)
        {
            if (enableEmit)
            {//初始加力
                gameobject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameobject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
                enableEmit = false;
            }
        }
        
    }

}