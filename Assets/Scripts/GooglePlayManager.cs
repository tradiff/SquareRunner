﻿using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class GooglePlayManager : MonoBehaviour
{
    void Awake()
    {
        PlayGamesPlatform.Activate();
    }

    // Use this for initialization
    void Start()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            // handle success or failure
            if (success)
            {
                Debug.Log("authenticated to google");
            }
            else
            {
                Debug.Log("failed to authenticate to google");
            }

        });
    }

    public void ReportScore(int score)
    {
        Social.ReportScore(score, "CgkIp4ihjYEFEAIQAA", (bool success) =>
        {
            if (success)
            {
                Debug.Log("reported score");
            }
            else
            {
                Debug.Log("failed to report score");
            }
        });
    }

    public void ShowLeaderBoards()
    {
        Social.ShowLeaderboardUI();
    }

}
