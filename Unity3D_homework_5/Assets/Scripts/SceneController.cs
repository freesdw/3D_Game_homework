using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour, ISceneController, IUserAction
{
    //game mode
    private ActionMode mode;

    public DiskFactory factory { get; set; }
    //the score 
    private int score;
    public int round;   //轮数  
    public int game = 0;//记录游戏进行情况 0 - ready; 1 - running; 2 - over;
    public int num = 0;//飞碟数量   
    public int loss = 0;
    //地面
    GameObject ground;

    void Awake()
 
    {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentScenceController = this;
        director.currentScenceController.LoadResources();
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
        if (GetScore() >= 20 + round * (round - 1) * 10)
        {
            round++;
        }
        else if (loss >= 10)
        {
        //    Debug.Log(sceneController.loss);
            game = 2;
        }
    }
    //开始游戏，将游戏状态设为1
    public void StartGame()   
    {
        num = 0;
        if (game == 0)
        {
            game = 1;  
        }
    }
    //重启游戏，初始化所有参数
    public void ReStart()  
    {
        game = 0;
        score = 0;
        round = 1;
        num = 0;
        loss = 0;
        //销毁物理动作管理和运动动作管理
        Destroy(this.gameObject.GetComponent<PhysicActionManager>());
        Destroy(this.gameObject.GetComponent<CCActionManager>());
    }
    //鼠标点击事件处理
    public void hit() 
    {
        //如果游戏在运行状态
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
    //载入资源  
    public void LoadResources()  
    {
        ground = Instantiate(Resources.Load("Prefabs/Ground"), new Vector3(0, -10, 0), Quaternion.identity) as GameObject;
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

    public ActionMode getMode()
    {
        return mode;
    }

    public void setMode(ActionMode am)
    {
        if(am == ActionMode.KINEMATIC)
        {
            // this.gameObject.GetComponent<CCActionManager>().enabled = true;
            this.gameObject.AddComponent<CCActionManager>();
        }
        else
        {
            //this.gameObject.GetComponent<PhysicActionManager>().enabled = true;
            this.gameObject.AddComponent<PhysicActionManager>();
        }
    }

    public void addLoss()
    {
        loss++;
    }
}