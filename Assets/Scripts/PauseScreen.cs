using System;
using UnityEngine;
using System.Collections;

public class PauseScreen : MonoBehaviour
{
    public void ResumeClick()
    {
        GameManager.Instance.ChangeState(GameManager.GameStates.Playing);
    }
    public void SettingsClick()
    {
        GameManager.Instance.GooglePlayManager.ShowLeaderBoards();
        //Debug.Log("SettingsClick");
        //GameManager.Instance.ChangeState(GameManager.GameStates.SetttingsScreen);
    }
    public void QuitClick()
    {
        GameManager.Instance.ChangeState(GameManager.GameStates.StartScreen);
    }
}
