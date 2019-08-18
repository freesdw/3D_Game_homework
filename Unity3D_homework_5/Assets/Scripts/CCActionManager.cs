using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//运动学动作管理类，总体实现类似物理学引擎管理类
public class CCActionManager : SSActionManager, ISSActionCallback, IActionManager
{
    public SceneController sceneController { set; get; }
    //飞盘工厂
    public DiskFactory diskFactory;
    //运动学动作
    public Move moveDisk;
    //飞盘对象
    public GameObject Disk;
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
            count++;
            if (count == 80)
            {
                playDisk();
                sceneController.num++;
               // Debug.Log("num:" + sceneController.num);
                //       print(sceneController.num);
                count = 0;
            }
            base.Update();
        }
    }

    public void playDisk()
    {

        moveDisk = Move.GetSSAction();
        Disk = diskFactory.getDisk(sceneController.GetRound());
        this.RunAction(Disk, moveDisk, this);
        Disk.GetComponent<DiskData>().action = moveDisk;
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, UnityEngine.Object objectParam = null)
    {
        diskFactory.freeDisk(source.gameobject);
        source.gameobject.GetComponent<DiskData>().hit = false;
    }
}