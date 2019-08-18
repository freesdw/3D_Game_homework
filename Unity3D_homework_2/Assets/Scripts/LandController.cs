using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandController {
    GameObject land;//对象

    Vector3[] pos;//用于放置人物对象的位置
    int side;//标志位，1 表示这是右边的陆地，-1表示这是左边的位置

    ChaController[] people;//陆地上的任务对象
    //初始化
    public LandController(GameObject t_land, int t_status)
    {
        pos = new Vector3[] {new Vector3(6.5f,2.5f,-2), new Vector3(7.5f,2.5f,-2), new Vector3(8.5f,2.5f,-2),
                new Vector3(9.5f,2.5f,-2), new Vector3(10.5f,2.5f,-2), new Vector3(11.5f,2.5f,-2)};

        people = new ChaController[6];

        land = t_land;
        side = t_status;
    }
    //获取人物位置数组中，没有人物的位置下标
    public int getEmptyIndex()
    {
        for(int i = 0; i < people.Length; i++)
        {
            if (people[i] == null) return i;
        }
        return -1;
    }
    //获取人物位置数组中，没有人物的位置坐标
    public Vector3  getEmptyPosition()
    {
        Vector3 position = pos[getEmptyIndex()];
        position.x *= side;
        return position;
    }
    //人物上岸，找个没人的位置给他
    public void getOnLand(ChaController cha)
    {
        int index = getEmptyIndex();
        people[index] = cha;
    }
    //人物上船
    public ChaController getOffLand(string person)
    {
        for(int i = 0; i < people.Length; i++)
        {
            if(people[i] != null && people[i].getChaName() == person)
            {
                ChaController chaCon = people[i];
                people[i] = null;
                return chaCon;
            }
        }
        return null;
    }
    //获取当前陆地的标识（左边还是右边）
    public int getSide()
    {
        return side;
    }
    //获取陆地上牧师的数目和魔鬼的数目
    public int[] getChaNum()
    {
        int[] count = {0,0};
        foreach(ChaController chac in people)
        {
            if(chac != null)
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
        people = new ChaController[6];
    }
}
