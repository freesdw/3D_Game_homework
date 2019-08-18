using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController  {
    GameObject boat;//对象
   // MoveStatus moveable;//移动
    Vector3[] from_pos;//船上的能坐人的位置，右边
    Vector3[] to_pos;//船上能坐人的位置，左边

    int onWhere;//标志位，船在左边还是右边(1 - 右边， -1 - 左边）
    ChaController[] people = new ChaController[2];//船上的人物

    private float speed = 15;//船的移动速度

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    public BoatController(GameObject t_boat)
    {
        onWhere = 1;//初始在右边

        from_pos = new Vector3[] { new Vector3(4.5f, 1.65f, -2), new Vector3(5.5f, 1.65f, -2) };
        to_pos = new Vector3[] { new Vector3(-5.5f, 1.65f, -2), new Vector3(-4.5f, 1.65f, -2) };

        boat = t_boat;

     //   moveable = boat.AddComponent(typeof(MoveStatus)) as MoveStatus;
        boat.AddComponent(typeof(ClickGUI));
    }
    /// <summary>
    /// 船的移动。每移动一次，改变标志位
    /// </summary>
    //public void Move()
    //{
    //    if (onWhere == -1)
    //    {
    //          moveable.setDestination(new Vector3(5, 1.15f, -2.5f));
    //        onWhere = 1;
    //    }
    //    else
    //    {
    //         moveable.setDestination(new Vector3(-5, 1.15f, -2.5f));
    //        onWhere = -1;
    //    }
    //}

        //获取船将要去的位置
    public Vector3 getDestination()
    {
       // Debug.Log("on boat:" + onWhere);
        if(onWhere == -1)
        {
            onWhere = 1;
            return new Vector3(5, 1.15f, -2.5f);
        }
        else
        {
            onWhere = -1;
            return new Vector3(-5, 1.15f, -2.5f);
        }
    }

    //获取船上的空位
    public int getEmptyIndex()
    {
        for(int i = 0; i < people.Length; i++)
        {
            if (people[i] == null) return i;
        }
        return -1;
    }
    //判断船是否没人
    public bool isEmpty()
    {
        foreach(ChaController chac in people)
        {
            if (chac != null) return false;
        }
        return true;
    }
    //获取没人的位置的坐标
    public Vector3 getEmptyPos()
    {
        int index = getEmptyIndex();
        if(onWhere == -1)
        {
            return to_pos[index];
        }
        else
        {
            return from_pos[index];
        }
    }
    //人物对象上船
    public void getOnBoat(ChaController chac)
    {
        int index = getEmptyIndex();
        people[index] = chac;
    }
    /// <summary>
    /// 人物对象下船
    /// </summary>
    /// <param name="name">下船的人的名字</param>
    /// <returns>返回这个对象的控制器</returns>
    public ChaController getOffBoat(string name)
    {
        for(int i = 0; i < people.Length; i++)
        {
            if(people[i] != null && people[i].getChaName() == name)
            {
                ChaController chac = people[i];
                people[i] = null;
                return chac;
            }
        }
        return null;
    }

    public GameObject getGameobj()
    {
        return boat;
    }

    public int getOnWhere()
    {
        return onWhere;
    }
    //获取船上各身分人物的数量
    public int[] getChaNum()
    {
        int[] count = { 0, 0 };
        foreach (ChaController chac in people)
        {
            if (chac != null)
            {
                int add = chac.getChaType() == 0 ? 0 : 1;
                count[add]++;
            }
        }
        return count;
    }
    //重置
    public void reset()
    {
       // moveable.Reset();
        if (onWhere == -1)
        {
            onWhere = 1;
            boat.transform.position = new Vector3(5, 1.15f, -2.5f);
            //moveable.setDestination(new Vector3(5, 1.15f, -2.5f));
        }
        people = new ChaController[2];
    }
}
