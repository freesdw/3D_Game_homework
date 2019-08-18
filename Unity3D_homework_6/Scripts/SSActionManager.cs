using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//动作管理类，基本就是一个模板
public class SSActionManager : MonoBehaviour, ISSActionCallback
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();   
    private List<SSAction> waitingAdd = new List<SSAction>();                   
    private List<int> waitingDelete = new List<int>();                  

    protected void Update()
    {
        foreach (SSAction ac in waitingAdd)
        {
            actions[ac.GetInstanceID()] = ac;
        }
        waitingAdd.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.destroy)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if (ac.enable)
            {
                ac.Update();
            }
        }

        foreach (int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        waitingDelete.Clear();
    }
 //增加动作
    public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
    {
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
    }
    //针对这次游戏新增的一个方法，用于增加不同的动作
    public void SSActionEvent(SSAction source, bool catching = false, GameObject objectParam = null)
    {
        //不捉玩家时，自由移动
        if(!catching)
        {
            GoPatrolAction goPatrolAction = GoPatrolAction.GetSSAction(objectParam.gameObject.GetComponent<PatrolData>().start_position);
            RunAction(objectParam, goPatrolAction, this);
            Singleton<GameEventManager>.Instance.PlayerEscape();
        }
        //捕捉玩家时，跟着玩家走
        else
        {
            PatrolFollowAction patrolFollowAction = PatrolFollowAction.GetSSAction(objectParam.gameObject.GetComponent<PatrolData>().player);
            RunAction(objectParam, patrolFollowAction, this);
        }
    }

    //销毁所有动作
    public void DestroyAll()
    {
        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            ac.destroy = true;
        }
    }
}
