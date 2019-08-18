using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour {
    //物体的运动等待队列及等待序号，就绪队列
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    //动作等待队列
    private List<SSAction> waitingAdd = new List<SSAction>();
    //已完成的动作的等待删除队列
    private List<int> waitingDelete = new List<int>();

    protected void Update()
    {
        //将等待执行的动作及其执行顺序放入就绪队列中
        foreach (SSAction ac in waitingAdd)
            actions[ac.GetInstanceID()] = ac;
        //清空等待执行的动作队列
        waitingAdd.Clear();

        //将每个动作从就绪队列中拿出，执行，并将已执行完成的队列放入删除队列
        foreach(KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if(ac.destory)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if(ac.enable)
            {
                ac.Update();
            }
        }
        //删除已经执行了的队列
        foreach(int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        waitingDelete.Clear();
    }
    //动作执行，将要执行的动作放入等待队列，关联动作对象等
    public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
    {
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        action.callback = manager;
        waitingAdd.Add(action);
        action.Start();
        //Debug.Log(gameobject.ToString());
    }

    protected void Start()
    {
        
    }
}
