using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//挂在场景的门上,用来监听玩家是否已到出口
public class DoorColllision : MonoBehaviour {
    //玩家到达出口
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Singleton<GameEventManager>.Instance.PlayerInDoor();
        }
    }
    //玩家离开出口
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Singleton<GameEventManager>.Instance.PlayerInDoor();
        }
    }
}
