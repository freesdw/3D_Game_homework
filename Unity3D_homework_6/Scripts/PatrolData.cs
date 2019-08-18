using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolData : MonoBehaviour
{
    //标志巡逻兵区域
    public int sign;
    //是够跟随玩家
    public bool follow_player = false;
    //玩家所在区域
    public int areaSign = -1; 
    //玩家对象
    public GameObject player;
    //巡逻兵初始位置   
    public Vector3 start_position;        
}
