using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController
{
    //加载场景资源
    void LoadResources();
}

public interface IUserAction                          
{
    //移动玩家
    void MovePlayer(int dir);
    //得到分数
    int GetScore();
    //得到要是数目
    int getKeys();
    //重新开始
    void Restart();
}

public interface ISSActionCallback
{
    void SSActionEvent(SSAction source,bool catching = false,GameObject objectParam = null);
}

public interface IGameStatusOp
{
    void PlayerEscape();
    void PlayerGameover();
}
