using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于检测用户的活动
public interface UserAction
{
    void moveBoat();
    void isClickCha(ChaController chaController);
    void restart();
}
