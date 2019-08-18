using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager:SSActionManager, ISSActionCallback
{
    private FirstController sceneController;

    protected new void Start()
    {
        sceneController = (FirstController)Director.getInstance().currentSceneController;
        sceneController.actionManager = this;
    }

    void ISSActionCallback.SSActionEvent(SSAction source,
        SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0,
        string strParam = null,
        Object objectParam = null)
    {
        //不知道要实现些啥
    }

    public void moveBoat(BoatController boat)
    {
        CCMoveToAction action = CCMoveToAction.GetSSAction(boat.getDestination(), boat.Speed);
       // Debug.Log(boat.getDestination());
        this.RunAction(boat.getGameobj(), action, this);
    }

    public void moveCharacter(ChaController characterCtrl, Vector3 destination)
    {
        Vector3 halfDest = destination;
        Vector3 currentPos = characterCtrl.getPosition();
        if (destination.y > currentPos.y)
        {
            halfDest.x = currentPos.x;

        }
        //上船时转折处理
        else
        {
            halfDest.y = currentPos.y;
        }
        SSAction action1 = CCMoveToAction.GetSSAction(halfDest, characterCtrl.Speed);
        SSAction action2 = CCMoveToAction.GetSSAction(destination, characterCtrl.Speed);
        SSAction seqAction = CCSequenceAction.GetSSAction(1, 0, new List<SSAction> { action1, action2 });
        this.RunAction(characterCtrl.getCharacter(), seqAction, this);
    }

    public new void Update()
    {
        base.Update();
    }
}
