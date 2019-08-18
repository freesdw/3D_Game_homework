using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//玩家游戏界面
public class UserGUI : MonoBehaviour {
   private IUserAction action;
    
    void Start ()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
    }

    void Update()
    {
       //玩家操作，为增加难度，玩家每次只能上下左右其中一个方向
        if(FirstSceneController.gameStatus == 0)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                action.MovePlayer(0);
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                action.MovePlayer(2);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                action.MovePlayer(-1);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                action.MovePlayer(1);
            }
        }
        
    }
    //信息显示
    private void OnGUI()
    {
        GUIStyle number_style = new GUIStyle();
        number_style.fontSize = 20;
        number_style.normal.textColor = Color.red;

        GUIStyle words_style = new GUIStyle();
        words_style.fontSize = 20;
        words_style.normal.textColor = Color.blue;

        GUIStyle end_style = new GUIStyle();
        end_style.fontSize = 60;
        end_style.normal.textColor = Color.red;
        

        GUI.Label(new Rect(Screen.width / 2 - 100, 5, 50, 30), "分数:", words_style);
        GUI.Label(new Rect(Screen.width / 2 - 50, 5, 50, 30), action.GetScore().ToString(), number_style);
        GUI.Label(new Rect(Screen.width / 2 + 50, 5, 50, 30), "钥匙:", words_style);
        GUI.Label(new Rect(Screen.width / 2 + 100, 5, 50, 30), action.getKeys().ToString(), number_style);

        if(FirstSceneController.gameStatus == 2)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 -  50, 200, 100), "游戏失败", end_style))
            {
                action.Restart();
                return;
            }
        }
        if (FirstSceneController.gameStatus == 1)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "游戏胜利", end_style))
            {
                action.Restart();
                return;
            }
        }

        if (FirstSceneController.gameStatus == -1)
        {
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "小新因意外被困在了迷宫中\n现在，他必须找到三把钥匙，并走到出口，方可逃离\n每个房间中均有守卫，若被守卫捉住，游戏失败\n点击任意地方开始游戏..."))
            {
                FirstSceneController.gameStatus = 0;
            }
        }
    }
}
