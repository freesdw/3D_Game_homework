﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveToAction : SSAction {
    public Vector3 target;
    public float speed;

    public static CCMoveToAction GetSSAction(Vector3 target, float speed)
    {
        CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction>();
        //Debug.Log(target);
        action.target = target;
        action.speed = speed;
        return action;
    }
    public override void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
       // Debug.Log("po:" + this.transform.position);
        if(this.transform.position == target)
        {
            this.destory = true;
            this.callback.SSActionEvent(this);
        }
    }
    public override void Start()
    {

    }
}
