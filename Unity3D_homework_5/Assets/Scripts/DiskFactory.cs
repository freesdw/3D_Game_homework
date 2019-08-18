using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//飞碟生成及回收工厂
public class DiskFactory : MonoBehaviour
{
    private static DiskFactory _instance;
    public SceneController sceneController { get; set; } 
    //飞盘预制及属性
    GameObject diskPrefab;
    DiskData diskData;
    //正在使用的飞盘
    public List<GameObject> used;
    //空闲的飞盘
    public List<GameObject> free;
    //飞盘的颜色
    Color[] diskColor = { Color.green, Color.red, Color.yellow };

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = Singleton<DiskFactory>.Instance;
            _instance.used = new List<GameObject>();
            _instance.free = new List<GameObject>();
            //导入飞盘预制
            diskPrefab = Instantiate(Resources.Load<GameObject>("Prefabs/Disk"), new Vector3(0, -20, 0), Quaternion.identity);
        }
    }
    public void Start()
    {
        sceneController = (SceneController)SSDirector.getInstance().currentScenceController;          
        sceneController.factory = this;
    }

    public GameObject getDisk(int round) 
    {
        //if (sceneController.GetScore() >= 20 + round*(round - 1)*10)                          
        //{
        //    sceneController.round++;
        //}
        //else if (sceneController.loss >= 10)
        //{
        //    Debug.Log(sceneController.loss);
        //    sceneController.game = 2; 
        //}
        GameObject newDisk;
        //如果工厂中有空余的飞盘，则使用空余的，否则，新增飞盘
        if (free.Count == 0)
        {
       //     Debug.Log("here");
            newDisk = GameObject.Instantiate(diskPrefab) as GameObject;
        }
        else
        {
            newDisk = free[0];
            free.Remove(free[0]);
      //      Debug.Log("here");
        }
        //设置飞盘属性
        diskData = newDisk.GetComponent<DiskData>();                                
        diskData.color = diskColor[Random.Range(0, 3)];
        newDisk.GetComponent<Renderer>().material.color = diskData.color;
        used.Add(newDisk);
        return newDisk;
    }
    //飞盘回收
    public void freeDisk(GameObject disk1)
    {
        for (int i = 0; i < used.Count; i++)
        {
            if (used[i].GetInstanceID() == disk1.GetInstanceID())
            {
                used.Remove(disk1);
                disk1.SetActive(true);
                disk1.GetComponent<DiskData>().hit = false;
                free.Add(disk1);
            }
        }
        return;
    }
}