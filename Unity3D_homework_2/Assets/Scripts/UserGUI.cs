using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
    private UserAction action;
    //用于记录游戏的状态（1 - 输了，2 - 赢了， 3 - 还在玩）
    private int status = 0;
    GUIStyle style;
    GUIStyle buttonStyle;
    //属性封装
    public int Status
    {
        set
        {
            status = value;
        }
        get
        {
            return status;
        }
    }

    private void Start()
    {
        
        action = Director.getInstance().currentSceneController as UserAction;
        //label的样式
        style = new GUIStyle();
        style.fontSize = 50;
        style.normal.textColor = Color.red;
        //button的样式
        buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 30;
        buttonStyle.normal.textColor = Color.blue;
    }

    private void OnGUI()
    {
        if (status == 1)//输了
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 85, 150, 50), "Game Over!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
            {
                //重新开始游戏
                status = 0;
                action.restart();
            }
        }
        else if (status == 2)//赢了
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 85, 150, 50), "You win!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
            {
                status = 0;
                action.restart();
            }
        }
    }

}
