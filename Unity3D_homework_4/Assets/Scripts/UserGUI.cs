using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    bool showDetail = false;

    void Start()
    {
        action = SSDirector.getInstance().currentScenceController as IUserAction;
    }
    void OnGUI()
    {
        if(action.GetGameState() == 0)
        {
            if (GUI.Button(new Rect(0, 0, 120, 40), "Read me"))
            {
                showDetail = true;
                Debug.Log(showDetail);
            }
            if (GUI.Button(new Rect(0, 60, 120, 40), "START GAME"))
            {
                action.StartGame();
            }
        }
        else if(action.GetGameState() != 2)
        {
            if (GUI.Button(new Rect(0, 120, 120, 40), "RESTART"))
            {
                action.ReStart();
            }

            GUIStyle fontStyle = new GUIStyle();
            fontStyle.normal.textColor = Color.blue;
            fontStyle.fontSize = 20;
            string text1 = "Score:" + action.GetScore();
            string text2 = "Round:" + action.GetRound().ToString();
            GUI.Label(new Rect(Screen.width / 2 - 100, 0, 100, 40), text1, fontStyle);
            GUI.Label(new Rect(Screen.width / 2, 0, 100, 40), text2, fontStyle);
        }
        if(action.GetGameState() == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                action.hit();
            }
        }
        if(showDetail)
        {
            Debug.Log("here");
            if(GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "Use your mouse click the disk, \nyou will get 1 point for each Disk，\nthe Disk will have three colors,\nyou should get 20 points to pass round1,\n20 + 2*1*10 to pass round2,\n20+3*2*10 to pass round3.etc\nGood Luck!!!"))
            {
                showDetail = false;
            }
        }
        if (action.GetGameState() == 2)
        {
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "Game Over!\nPlease click anywhere to back to the menu!"))
            {
                action.ReStart();
            }
        }
    }


    // Update is called once per frame  
    void Update()
    {
        //  
    }
}
