using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour, ISceneController, IUserAction
{
    public CCActionManager actionManager;
    public DiskFactory factory { get; set; }

    private int score;
    public int round = 0;//轮数   
    public int game = 0;//记录游戏进行情况 0 - ready; 1 - running; 2 - over;
    public int num = 0;//飞碟数量   
    public int loss = 0;

    GameObject ground;

    void Awake()
 
    {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentScenceController = this;
        director.currentScenceController.LoadResources();
        actionManager = new CCActionManager();
    }
    void Start()
    {
        score = 0;
        round = 1; 
    }

    public float GetScore()
    {
        return score;
    }

    // Update is called once per frame  
    void Update()
    {
        loss = num - score;
    }

    public void StartGame()   
    {
        num = 0;
        if (game == 0)
        {
            game = 1;  
        }
    }
    public void ReStart()  
    {
        game = 0;
        score = 0;
        round = 1;
        num = 0;
        loss = 0;
    }
    
    public void hit() 
    {
        if (game == 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Disk")
                {
                    hit.collider.gameObject.SetActive(false);
                    hit.collider.gameObject.GetComponent<DiskData>().hit = true;
                    AddScore();
                }
            }
        }
    }
    public void LoadResources()  //载入资源  
    {
        ground = Instantiate(Resources.Load("prefabs/Ground"), new Vector3(0, -10, 0), Quaternion.identity) as GameObject;
        ground.name = "ground";
    }

    public int GetGameState()
    {
        return game;
    }

    public int GetRound()
    {
        return round;
    }

    public void AddScore()
    {
        score += 1;
    }

}
