using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback, IActionManager
{
    public SceneController sceneController { set; get; }
    public DiskFactory diskFactory;
    public Emit EmitDisk;
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
            if (count == 60)
            {
                playDisk();
                sceneController.num++;
                print(sceneController.num);
                count = 0;
            }
            base.Update();
        }
    }

    public void playDisk()
    {
       // Debug.Log("this");
        EmitDisk = Emit.GetSSAction();
        Disk = diskFactory.getDisk(sceneController.GetRound());
        this.RunAction(Disk, EmitDisk, this);
        Disk.GetComponent<DiskData>().action = EmitDisk;
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, UnityEngine.Object objectParam = null)
    {
        diskFactory.freeDisk(source.gameobject);
        source.gameobject.GetComponent<DiskData>().hit = false;
    }
}