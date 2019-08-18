using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropFactory : MonoBehaviour
{
    //巡逻兵
    private GameObject patrol = null;        
    //当前使用中的兵
    private List<GameObject> patrolList = new List<GameObject>();   
    //当前使用中的钥匙
    private List<GameObject> keyList = new List<GameObject>();                    

    public FirstSceneController sceneControler;          

    public List<GameObject> GetPatrols()
    {
        //巡逻兵的初始位置
        int[] posX = { -7, -6, -7, 4, 4, 4, 13, 13, 13 };
        int[] posZ = { -4, 6, -13, -4, 6, -13, -4, 6, -13 };
        //导入巡逻兵及设置其属性
        for (int i = 0; i < 9; i++)
        {
            patrol = Instantiate(Resources.Load<GameObject>("Prefabs/Patrol"));
            patrol.transform.position = new Vector3(posX[i], 0, posZ[i]);
            patrol.GetComponent<PatrolData>().sign = i + 1;
            patrol.GetComponent<PatrolData>().start_position = new Vector3(posX[i], 0, posZ[i]);
            patrolList.Add(patrol);
        }
        return patrolList;
    }

    //导入钥匙
    public List<GameObject> GetKeys()
    {
        GameObject key1 = Instantiate(Resources.Load<GameObject>("Prefabs/key"));
        key1.transform.position = new Vector3(-12, 0, 12);

        GameObject key2 = Instantiate(Resources.Load<GameObject>("Prefabs/key"));
        key2.transform.position = new Vector3(0, 0, -12);

        GameObject key3 = Instantiate(Resources.Load<GameObject>("Prefabs/key"));
        key3.transform.position = new Vector3(12, 0, 12);

        keyList.Add(key1);
        keyList.Add(key2);
        keyList.Add(key3);

        return keyList;
    }
    //巡逻者停止跑动
    public void StopRun()
    {
        foreach(GameObject patrol in patrolList)
        {
            patrol.GetComponent<Animator>().SetBool("run", false);
        }
    }
}
