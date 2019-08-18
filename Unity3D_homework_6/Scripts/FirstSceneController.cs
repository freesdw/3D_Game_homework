using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController
{
    //游戏状态  -1 - ready, 0 - running, 1 - win, 2 - loss
    public static int gameStatus = -1;
    //巡逻者工厂
    public PropFactory factory;
    //记录员
    public ScoreRecorder recorder;
    //运动管理器
    public PatrolActionManager actionManager;
    //当前玩家所处哪个格子
    public int areaSign = -1;
    //玩家
    public GameObject player;
    //玩家移动速度
    private float speed = 0.06f;
    //主相机
    public Camera camera;
    //场景中巡逻者列表
    private List<GameObject> patrols;   
    
    private bool temp = false;

    void Update()
    {
        //获取玩家当前所在的区域
        for (int i = 0; i < patrols.Count; i++)
        {
            patrols[i].gameObject.GetComponent<PatrolData>().areaSign = areaSign;
        }
        //巡逻兵动作初始
        if(!temp && gameStatus == 0)
        {
            //所有巡逻兵移动
            for (int i = 0; i < patrols.Count; i++)
            {
                actionManager.GoPatrol(patrols[i]);
            }
            temp = true;
        }

       // 游戏胜利：收集三个钥匙，并成功抵达出口
        if (recorder.GetKeyNumber() == 3 && recorder.getInDoor())
        {
            Gamewin();
        }
    }
    //设置各种人员
    void Start()
    {

        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        factory = Singleton<PropFactory>.Instance;
        actionManager = gameObject.AddComponent<PatrolActionManager>() as PatrolActionManager;
        LoadResources();
        camera.GetComponent<CameraFlow>().follow = player;
        recorder = Singleton<ScoreRecorder>.Instance;

    }
    //导入资源
    public void LoadResources()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Plane"), new Vector3(0, 0, 0), Quaternion.identity);
        player = Instantiate(Resources.Load("Prefabs/Player"), new Vector3(-10, 3, -10), Quaternion.identity) as GameObject;
        factory.GetKeys();
        patrols = factory.GetPatrols();    
    }
    //玩家移动
    public void MovePlayer(int dir)
    {
        if(gameStatus == 0)
        {
            //转向
            player.transform.rotation = Quaternion.Euler(new Vector3(0, dir * 90, 0));
            switch (dir)
            {
                case 0:
                    player.transform.position += new Vector3(0, 0, speed);
                    break;
                case 2:
                    player.transform.position += new Vector3(0, 0, -speed);
                    break;
                case -1:
                    player.transform.position += new Vector3(-speed, 0, 0);
                    break;
                case 1:
                    player.transform.position += new Vector3(speed, 0, 0);
                    break;
            }
        }
    }
    //获取分数
    public int GetScore()
    {
        return recorder.GetScore();
    }
    //获取钥匙数目
    public int GetKeyNumber()
    {
        return recorder.GetKeyNumber();
    }
    //重新加载场景
    public void Restart()
    {
        SceneManager.LoadScene("Scenes/mySence");
        gameStatus = -1;
        temp = false;
    }
    //事件管理
    void OnEnable()
    {
        GameEventManager.ScoreChange += AddScore;
        GameEventManager.GameoverChange += Gameover;
        GameEventManager.KeyChange += AddKeyNumber;
        GameEventManager.InDoor += InDoorChange;
    }
    void OnDisable()
    {
        GameEventManager.ScoreChange -= AddScore;
        GameEventManager.GameoverChange -= Gameover;
        GameEventManager.KeyChange -= AddKeyNumber;
        GameEventManager.InDoor -= InDoorChange;
    }
    //增加钥匙数量
    void AddKeyNumber()
    {
        recorder.AddKeys();
    }
    //增加分数
    void AddScore()
    {
        recorder.AddScore();
    }
    //改变是否在出口状态
    void InDoorChange()
    {
        recorder.InDoorChange();
    }
    //游戏胜利
    void Gamewin()
    {
        gameStatus = 1;
        actionManager.DestroyAllAction();
    }
    //游戏失败
    void Gameover()
    {
        gameStatus = 2;
        actionManager.DestroyAllAction();
    }
    //获取钥匙数目
    public int getKeys()
    {
        return recorder.GetKeyNumber();
    }
}
