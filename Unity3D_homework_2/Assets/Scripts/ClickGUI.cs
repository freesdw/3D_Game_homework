using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickGUI : MonoBehaviour {
    UserAction action;
    ChaController chac;
    //设置点击的人物
    public void setController(ChaController chaController)
    {
        chac = chaController;
    }

    private void Start()
    {
        action = Director.getInstance().currentSceneController as UserAction;
    }
    //鼠标点击事件
    private void OnMouseUp()
    {
        try
        {
            action.isClickCha(chac);
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
        finally
        {
            Debug.Log("Clicking:" + gameObject.name);
        }
        
    }

    private void OnGUI()
    {
        //开船事件。由于用鼠标点击的时候，总是点不中，就直接用button来了
        if ((Director.getInstance().currentSceneController as FirstController).isOver() == 0)
        {
            GUIStyle buttonStyle = new GUIStyle("button");
            buttonStyle.fontSize = 30;
            buttonStyle.normal.textColor = Color.blue;
            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 25, 120, 50), "set sail", buttonStyle))
            {
                    action.moveBoat();
            }
        }
        //加点儿游戏的文字说明
        GUIStyle gUIStyle = new GUIStyle();
        gUIStyle.fontSize = 40;
        gUIStyle.normal.textColor = Color.green;
        GUI.Label(new Rect(Screen.width / 2 - 150, 10, 300, 40), "Priests And Devils", gUIStyle);

        GUI.Label(new Rect(10, 200, 300, 300), "Priests and Devils is a " +
            "game in which you will help the Priests and Devils to cross the river." +
            " There are 3 priests and 3 devils at one side of the river." +
            " They all want to get to the other side of this river, " +
            "but there is only one boat and this boat can only carry two persons each time." +
            " And there must be one person steering the boat from one side to the other side." +
            " In this game, you can click on them to move them and click the button to move the boat to the other direction. " +
            "If the priests are out numbered by the devils on either side of the river, they get killed and the game is over." +
            " You can try it in many ways. Keep all priests alive! Good luck!");
    }
}
