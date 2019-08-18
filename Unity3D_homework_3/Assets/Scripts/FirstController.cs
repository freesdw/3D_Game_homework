using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, SceneController, UserAction {

    UserGUI userGUI;

    public LandController fromLand;
    public LandController toLand;
    public BoatController boat;
    private ChaController[] people;

    public CCActionManager actionManager;

    private Transform background;
    //加点声音
    public AudioClip audio;
    private AudioSource audioSource;

    //初始化游戏场景和配置。
    void Awake()
    {
        Director.getInstance().currentSceneController = this;
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        people = new ChaController[6];
        actionManager = new CCActionManager();

        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.volume = 1.0f;
        audioSource.clip = audio;

        LoadResources();
    }
    //创建对象的辅助函数
    private GameObject createObject(string name, Vector3 position)
    {
        return (GameObject)Object.Instantiate(Resources.Load("Prefabs/" + name), position, Quaternion.identity);
    }
    //导入各种预制体资源
    public void LoadResources()
    {
        //背景设置（其实就是一个方块加点贴图。能力限制，就这么弄了）
        background = Instantiate<Transform>(Resources.Load<Transform>("Prefabs/backGround"), new Vector3(0, 6, 3), Quaternion.identity);
        background.name = "background";
        background.localScale += new Vector3(35, 20, 2);
        background.Rotate(new Vector3(10, 0, 180));
        
        //导入陆地、河流和船
        GameObject river = createObject("River", new Vector3(0, 0, -2));
        river.name = "river";

        GameObject leftLand = createObject("Land", new Vector3(-10, 0.5f, -2));
        leftLand.name = "leftLand";

        GameObject rightLand = createObject("Land", new Vector3(10, 0.5f, -2));
        rightLand.name = "rightLand";

        GameObject t_boat = createObject("Boat", new Vector3(5, 1.15f, -2.5f));
        t_boat.name = "boat";

        //设置控制器
        fromLand = new LandController(rightLand, 1);
        toLand = new LandController(leftLand, -1);
        boat = new BoatController(t_boat);
        //导入游戏人物对象并设置控制器
        for (int i = 0; i < 3; i++)
        {
            GameObject temp = createObject("devil", Vector3.zero);
            ChaController cha = new ChaController(temp, 1);
            cha.setName("devil" + i);
            cha.setPosition(fromLand.getEmptyPosition());
            cha.getOnLand(fromLand);
            fromLand.getOnLand(cha);

            people[i] = cha;
        }

        for (int i = 0; i < 3; i++)
        {
            GameObject temp = createObject("Priests", Vector3.zero);
            ChaController cha = new ChaController(temp, 0);

            cha.setName("priest" + i);
            cha.setPosition(fromLand.getEmptyPosition());
            cha.getOnLand(fromLand);
            fromLand.getOnLand(cha);

            people[i + 3] = cha;
        }
    }
    //船的移动，当船上有人时，船才可以移动。。
    public void moveBoat()
    {
        if (!boat.isEmpty())
            // boat.Move();
            actionManager.moveBoat(boat);
        userGUI.Status = isOver();
    }
    /// <summary>
    /// 如果点了某个人物，判断其是在船上还是陆地上
    /// 如果在船上，则上岸；否则，上船
    /// </summary>
    /// <param name="chac">某个人</param>
    public void isClickCha(ChaController chac)
    {
        //上岸
        if(chac.isOnBoat())
        {
            LandController whichLand;
            if (boat.getOnWhere() == -1)
                whichLand = toLand;
            else
                whichLand = fromLand;

            boat.getOffBoat(chac.getChaName());
            //chac.movePosition(whichLand.getEmptyPosition());
           // chac.setDestination(whichLand.getEmptyPosition());
            actionManager.moveCharacter(chac, whichLand.getEmptyPosition());//动作执行
            chac.getOnLand(whichLand);
            whichLand.getOnLand(chac);
        }
        //上船
        else
        {
            LandController whichLand = chac.getLandCont();
            if (boat.getEmptyIndex() == -1) return;
            if (whichLand.getSide() != boat.getOnWhere()) return;

            whichLand.getOffLand(chac.getChaName());
            //chac.movePosition(boat.getEmptyPos());
           // chac.setDestination(boat.getEmptyPos());
            actionManager.moveCharacter(chac, boat.getEmptyPos());//动作执行
            chac.getOnBoat(boat);
            boat.getOnBoat(chac);
        }
        userGUI.Status = isOver();//判断游戏是否已经达到了结束的条件
    }
    /// <summary>
    /// 判断游戏是否结束
    /// </summary>
    /// <returns>2 - 赢了； 1 - 输了； 0 - 还没结束</returns>
    public int isOver()
    {
        int fromP = 0;//右边牧师人数
        int fromD = 0;//右边魔鬼人数
        int toP = 0;//左边牧师人数
        int toD = 0;//左边魔鬼人数
        //获取右边对应的人数
        int[] fromCount = fromLand.getChaNum();
        fromP += fromCount[0];
        fromD += fromCount[1];
        //获取左边对应的人数
        int[] toCount = toLand.getChaNum();
        toP += toCount[0];
        toD += toCount[1];
        //如果左边有六个人，证明全部安全过河，你赢了
        if (toP + toD == 6) return 2;
        //将船上人的数目也加上去
        int[] boatCount = boat.getChaNum();
        if(boat.getOnWhere() == -1)
        {
            toP += boatCount[0];
            toD += boatCount[1];
        }
        else
        {
            fromP += boatCount[0];
            fromD += boatCount[1];
        }
        //如果任意一边牧师人数少于魔鬼，你输了
        if (fromP < fromD && fromP > 0 || toP < toD && toP > 0) return 1;
        return 0;
    }
    //重置游戏
    public void restart()
    {
        audioSource.Play();
        boat.reset();
        fromLand.reset();
        toLand.reset();
        foreach (ChaController chac in people) chac.Reset();
    }
}
