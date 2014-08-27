using UnityEngine;
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
                Social.ReportScore(1, "CgkIp4ihjYEFEAIQAA", (bool success2) =>
                {
                    if (success2)
                    {
                        Debug.Log("reported score");
                    }
                    else
                    {
                        Debug.Log("failed to report score");
                    }

                });
            }
            else
            {
                Debug.Log("failed to authenticate to google");
            }

        });
    }

    public void ShowLeaderBoards()
    {
        Social.ShowLeaderboardUI();
    }

}
