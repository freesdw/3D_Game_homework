using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//记分员，记录分数和各种必要信息
public class ScoreRecorder : MonoBehaviour
{
    public FirstSceneController sceneController;
    //分数
    public int score = 0;        
    //已找寻了的钥匙的数目
    public int keys = 0;    
    //是否在出口前
    private bool inDoor = false;

    // Use this for initialization
    void Start()
    {
        sceneController = SSDirector.GetInstance().CurrentScenceController as FirstSceneController;
        sceneController.recorder = this;
    }
    //获取当前分数
    public int GetScore()
    {
        return score;
    }
    //增加分数
    public void AddScore()
    {
        score++;
    }
    //是否在出口前或离开了出口
    public void InDoorChange()
    {
        inDoor = inDoor ? false : true;
    }
    //是否在出口前
    public bool getInDoor()
    {
        return inDoor;
    }
    //已找到的钥匙数
    public int GetKeyNumber()
    {
        return keys;
    }
    //增加钥匙数
    public void AddKeys()
    {
        keys++;
    }
}

