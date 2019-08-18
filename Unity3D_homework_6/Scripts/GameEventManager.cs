using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    //分数变化
    public delegate void ScoreEvent();
    public static event ScoreEvent ScoreChange;
    //游戏结束变化
    public delegate void GameoverEvent();
    public static event GameoverEvent GameoverChange;
    //钥匙数量变化
    public delegate void KeyEvent();
    public static event KeyEvent KeyChange;
    //玩家是否在出口前
    public delegate void InDoorEvent();
    public static event InDoorEvent InDoor;

    //玩家逃脱
    public void PlayerEscape()
    {
        if (ScoreChange != null)
        {
            ScoreChange();
        }
    }
    //玩家被捕
    public void PlayerGameover()
    {
        if (GameoverChange != null)
        {
            GameoverChange();
        }
    }
   // 增加钥匙数量
    public void AddKeyNumber()
    {
        if (KeyChange != null)
        {
            KeyChange();
        }
    }
    //更改玩家是否在出口前的状态
    public void PlayerInDoor()
    {
        if (InDoor != null)
        {
            InDoor();
        }
    }
}
