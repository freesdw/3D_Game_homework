using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//运动学动作基本类
public class Move : SSAction {
    //标记是否还可以继续移动
    bool enableMove = true;

 //开始出发的x轴位置
    float startX;
    public SceneController sceneController;
    //飞行的速度
    private float speed;
    //重力模仿
    private float gravitySim = 9.8f;
    //飞行的方向
    private Vector3 direction;
    //辅助计时
    private float time;

    public override void Start()
    {
        sceneController = (SceneController)SSDirector.getInstance().currentScenceController;
        //随机生成开始位置
        startX = 6 + Random.value * 12;

        direction = new Vector3(6 * Random.Range(-1, 1), 0, 12 * Random.Range(10, 15));
        time = 0;
        //根据关数生成速度
        speed = sceneController.round * 0.15f;
        this.transform.position = new Vector3(startX, 0, 0);
        //由于是运动学，关闭重力影响
        gameobject.GetComponent<Rigidbody>().useGravity = false;
    }
    //获取动作
    public static Move GetSSAction()
    {
        Move action = ScriptableObject.CreateInstance<Move>();
        return action;
    }

    public override void Update()
    {
        //如果动作还没到销毁的时候，一直执行
        if (!this.destroy)
        {
            if (enableMove)
            {
                time += Time.deltaTime;
                //重力模仿
                this.gameobject.transform.Translate(Vector3.down * gravitySim * time * Time.deltaTime);
                //水平方向的运动
                this.gameobject.transform.Translate(direction * speed * Time.deltaTime);
            }
        } 
        //当飞碟下降到一定高度后，失去分数，重置飞盘参数方便回收
        if(this.gameobject.transform.position.y < -12)
        {
            enableMove = false;
            enable = false;
            destroy = true;
            this.callback.SSActionEvent(this);
            sceneController.addLoss();
        }
    }
    //销毁这个动作
    public void Destory()
    {
        this.destroy = true;
        this.callback.SSActionEvent(this);
    }
}
