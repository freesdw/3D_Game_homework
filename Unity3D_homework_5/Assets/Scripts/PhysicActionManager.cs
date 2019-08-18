using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//物理引擎动作管理类
public class PhysicActionManager : SSActionManager, ISSActionCallback, IActionManager {
    public SceneController sceneController { set; get; }
    public DiskFactory diskFactory;
    //投掷动作
    public Emit EmitDisk;
    public GameObject Disk;
    //计时
    int count = 0;

    private void Start()
    {
        sceneController = (SceneController)SSDirector.getInstance().currentScenceController;
        diskFactory = sceneController.factory;
    }

    public new void Update()
    {
        if (sceneController.GetGameState() == 1)
        {
            //每80帧投掷一个飞盘
            count++;
            if (count == 80)
            {
                playDisk();
                sceneController.num++;
                //计数重置
                count = 0;
            }
            base.Update();
        }
    }
    //投掷飞盘
    public void playDisk()
    {
        // Debug.Log("this");
        EmitDisk = Emit.GetSSAction();
        //从工厂中获取飞盘
        try
        {
            Disk = diskFactory.getDisk(sceneController.GetRound());
        } catch (System.Exception e)
        {
            Debug.Log("get disk failed");
        }
        //添加动作进管理器
        this.RunAction(Disk, EmitDisk, this);
        Disk.GetComponent<DiskData>().action = EmitDisk;
    }
    //飞盘回收
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, UnityEngine.Object objectParam = null)
    {
        diskFactory.freeDisk(source.gameobject);
        source.gameobject.GetComponent<DiskData>().hit = false;
    }
}
