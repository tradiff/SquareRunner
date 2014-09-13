using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class GooglePlayManager : MonoBehaviour
{
    void Awake()
    {
        var a = PlayGamesPlatform.Activate();
    }

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("SocialOptOut", 0) == 1)
            return;

        Authenticate();
    }

    private void Authenticate()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            // handle success or failure
            if (success)
            {
                Debug.Log("authenticated to google");
                PlayerPrefs.SetInt("SocialOptOut", 0);
            }
            else
            {
                Debug.Log("failed to authenticate to google");
                PlayerPrefs.SetInt("SocialOptOut", 1);
            }
        });
    }

    public void ReportScore(int score)
    {
        var currentUserId = PlayGamesPlatform.Instance.GetUserId();
        if (currentUserId == "108775061000885810356") return; // travis
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
        Authenticate();
        PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIp4ihjYEFEAIQAA");
    }

}
