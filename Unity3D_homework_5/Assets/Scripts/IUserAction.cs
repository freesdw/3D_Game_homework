using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionMode { PHYSIC, KINEMATIC}

public interface IUserAction
{
    ActionMode getMode();
    void setMode(ActionMode mode);
    void StartGame();
    //void ShowDetail();
    void ReStart();
    void hit();
    int GetGameState();
    float GetScore();
    int GetRound();
}