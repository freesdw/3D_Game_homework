using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    private static DiskFactory _instance;
    public SceneController sceneController { get; set; } 
    GameObject diskPrefab;
    DiskData diskData;
    public List<GameObject> used;
    public List<GameObject> free;

    Color[] diskColor = { Color.green, Color.red, Color.yellow };

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = Singleton<DiskFactory>.Instance;
            _instance.used = new List<GameObject>();
            _instance.free = new List<GameObject>();
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
        if (sceneController.GetScore() >= 20 + round*(round - 1)*10)                          
        {
            sceneController.round++;
        }
        else if (sceneController.loss >= 20)
        {
            sceneController.game = 2; 
        }
        GameObject newDisk;
        if (free.Count == 0)
        {
            newDisk = GameObject.Instantiate(diskPrefab) as GameObject;
        }
        else
        {
            newDisk = free[0];
            free.Remove(free[0]);
        }
        diskData = newDisk.GetComponent<DiskData>();                                                     
        diskData.color = diskColor[Random.Range(0, 3)];
        newDisk.GetComponent<Renderer>().material.color = diskData.color;
        used.Add(newDisk);
        return newDisk;
    }

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
