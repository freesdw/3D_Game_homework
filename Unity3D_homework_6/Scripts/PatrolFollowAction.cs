using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFollowAction : SSAction
{
    //跟随玩家的速度
    private float speed = 1.5f;           
    private GameObject player;
    //侦查兵数据
    private PatrolData data;        

    private PatrolFollowAction() { }
    //获取跟随动作
    public static PatrolFollowAction GetSSAction(GameObject player)
    {
        PatrolFollowAction action = CreateInstance<PatrolFollowAction>();
        action.player = player;
        return action;
    }

    public override void Start()
    {
        data = this.gameobject.GetComponent<PatrolData>();
    }

    public override void Update()
    {
        //跟随玩家
        transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        this.transform.LookAt(player.transform.position);
        //如果玩家离开了巡逻兵的管理区域，停止跟随
        if (data.areaSign != data.sign)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this, false, this.gameobject);
        }
    }
}
