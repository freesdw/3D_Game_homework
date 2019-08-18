using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IUserAction
{
    void StartGame();
    //void ShowDetail();
    void ReStart();
    void hit();
    int GetGameState();
    float GetScore();
    int GetRound();
}