using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPatrolAction : SSAction
{
    //初始位置
    private float posX, posZ;
    //行走边长
    private float moveLength = 5;
    //移动速度
    private float speed = 1.2f; 
    //是否要转弯
    private bool turn = true;  
    //移动的方向
    private int dirction = 0;
    //属性
    private PatrolData data;       
    

    private GoPatrolAction() { }
    public static GoPatrolAction GetSSAction(Vector3 location)
    {
        GoPatrolAction action = CreateInstance<GoPatrolAction>();
        //设置初始位置
        action.posX = location.x;
        action.posZ = location.z;

        return action;
    }
    public override void Update()
    {
        //移动
        FreeMove();
        //如果侦察兵需要跟随玩家并且玩家就在侦察兵所在的区域，侦查动作结束
        if (data.follow_player && data.areaSign == data.sign)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this,true,this.gameobject);
        }
    }
    public override void Start()
    {
        data  = this.gameobject.GetComponent<PatrolData>();
    }

    void FreeMove()
    {
        if (turn)
        {
            //转向处理
            switch (dirction)
            {
                case 0:
                    posX -= moveLength;
                    break;
                case 1:
                    posZ += moveLength;
                    break;
                case 2:
                    posX += moveLength;
                    break;
                case 3:
                    posZ -= moveLength;
                    break;
            }
            turn = false;
        }

        Vector3 target = new Vector3(posX, 0, posZ);
        this.transform.LookAt(target);
        float distance = Vector3.Distance(transform.position, new Vector3(posX, 0, posZ));
        //移动
        if (Vector3.Distance(transform.position, target) > 0.5)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            dirction = (dirction + 1) % 4;
            turn = true;
        }
    }

}
