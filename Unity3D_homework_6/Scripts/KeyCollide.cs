using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//放在钥匙上，用来监听玩家是否获得钥匙
public class KeyCollide : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
            Singleton<GameEventManager>.Instance.AddKeyNumber();
        }
    }
}
