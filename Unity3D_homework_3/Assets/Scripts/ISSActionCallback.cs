using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SSActionEventType:int { Started, Competeted }


public interface ISSActionCallback
{
    //还没理解怎么用的。先从PPT上抄下来再说
    void SSActionEvent(SSAction source,
        SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0,
        string strParam = null,
        Object objectParam = null);
}
