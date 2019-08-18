using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaController {

    GameObject character;//游戏人物控制器
    MoveStatus moveable;//人物动作
    ClickGUI clickGUI;//点击事件
    private int whatCharacter;//人物的角色（牧师还是魔鬼）

    private bool onBoat;//人数是否在船上
    LandController landController;
    //人物控制器的初始化
    public ChaController(GameObject chac, int status)
    {
        character = chac;
        whatCharacter = status;

        moveable = character.AddComponent(typeof(MoveStatus)) as MoveStatus;

        clickGUI = character.AddComponent(typeof(ClickGUI)) as ClickGUI;
        clickGUI.setController(this);
    }

    public void setName(string name)
    {
        character.name = name;
    }
    //设置人物的位置
    public void setPosition(Vector3 position)
    {
        character.transform.position = position;
    }
    //人物移动事件
    public void movePosition(Vector3 dest)
    {
        moveable.setDestination(dest);
    }

    public int getChaType()
    {
        return whatCharacter;
    }

    public string getChaName()
    {
        return character.name;
    }
    //人物上船。设置船为人物的父对象
    public void getOnBoat(BoatController boat)
    {
        landController = null;
        character.transform.parent = boat.getGameobj().transform;
        onBoat = true;
    }
    //人物下船
    public void getOnLand(LandController land)
    {
        landController = land;
        character.transform.parent = null;
        onBoat = false;
    }

    public bool isOnBoat()
    {
        return onBoat;
    }

    public LandController getLandCont()
    {
        return landController;
    }
    //重置
    public void Reset()
    {
        moveable.Reset();
        landController = (Director.getInstance().currentSceneController as FirstController).fromLand;
        getOnLand(landController);
        setPosition(landController.getEmptyPosition());
        landController.getOnLand(this);
    }
}
