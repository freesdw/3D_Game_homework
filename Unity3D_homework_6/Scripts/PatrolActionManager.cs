using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolActionManager : SSActionManager
{    //巡逻兵巡逻
    private GoPatrolAction go_patrol;                        

    public void GoPatrol(GameObject patrol)
    {
        go_patrol = GoPatrolAction.GetSSAction(patrol.transform.position);
        this.RunAction(patrol, go_patrol, this);
    }
}
