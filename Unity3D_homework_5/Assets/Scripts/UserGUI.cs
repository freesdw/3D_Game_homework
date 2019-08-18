using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the GUI class
public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    //a tag used to show the game rules
    bool showDetail = false;

    void Start()
    {
        action = SSDirector.getInstance().currentScenceController as IUserAction;
    }
    void OnGUI()
    {
        //if the game is in ready
        if(action.GetGameState() == 0)
        {
            //the button used to show the game rules.
            if (GUI.Button(new Rect(0, 0, 120, 40), "Read me"))
            {
                showDetail = true;
              //  Debug.Log(showDetail);
            }
            //choose the physics mode
            if (GUI.Button(new Rect(0, 60, 120, 40), "Physics Mode"))
            {
                //set the mode
                action.setMode(ActionMode.PHYSIC);
                action.StartGame();
            }
            //choose the kinematics mode
            if(GUI.Button(new Rect(0, 120, 120, 40), "Kinematics Mode")) {
                //set the mode 
                action.setMode(ActionMode.KINEMATIC);
                action.StartGame();
            }
        }
        //if the game isn't over, show the score and the round
        else if(action.GetGameState() != 2)
        {
            //restart the game
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
        //running
        if(action.GetGameState() == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                action.hit();
            }
        }
        //show the detail 
        if(showDetail)
        {
            Debug.Log("here");
            if(GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "Use your mouse click the disk, \nyou will get 1 point for each Disk，\nthe Disk will have three colors,\nyou should get 20 points to pass round1,\n20 + 2*1*10 to pass round2,\n20+3*2*10 to pass round3.etc\nGood Luck!!!"))
            {
                showDetail = false;
            }
        }
        //game over
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