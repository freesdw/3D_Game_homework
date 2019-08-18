using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//放在巡逻兵上，当与玩家碰撞时，游戏结束
public class PlayerCollide : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("death");
            Singleton<GameEventManager>.Instance.PlayerGameover();
        }
    }
}
