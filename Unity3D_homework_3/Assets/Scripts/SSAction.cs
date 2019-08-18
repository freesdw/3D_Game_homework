using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSAction : ScriptableObject {
    public bool enable = true;//动作是否可以执行
    public bool destory = false;//动作是否可以销毁

    //动作的基本属性，动作对象等
    public GameObject gameobject { get; set; }
    public Transform transform { get; set; }
    public ISSActionCallback callback { get; set; }

    protected SSAction() { }
    public virtual void Start()
    {
        throw new System.NotImplementedException();
    }
    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }
}
