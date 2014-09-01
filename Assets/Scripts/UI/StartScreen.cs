using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public GooglePlayManager GooglePlayManager;
    GameObject TouchTarget;
    Text versionText;

    // Use this for initialization
    void Awake()
    {
        GooglePlayManager = transform.GetComponent<GooglePlayManager>();
        TouchTarget = GameObject.Find("TouchTarget");
        Time.timeScale = 1;
        versionText = GameObject.Find("VersionText").GetComponent<Text>();
        versionText.text = "v " + CurrentBundleVersion.version;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.StartTouch())
        {
            var screenPos = InputManager.Instance.GetTouch();
            Vector3 wp = Camera.main.ScreenToWorldPoint(screenPos);
            Vector2 touchPos = new Vector2(wp.x, wp.y);

            Collider2D collider2d = Physics2D.OverlapPoint(touchPos);
            if (collider2d == TouchTarget.collider2D)
            {
                Debug.Log("Loading level");
                Application.LoadLevel("Level");
            }
        }
    }

    public void LeaderboardsClick()
    {
        GooglePlayManager.ShowLeaderBoards();
    }

    public void ShopClick()
    {
        Debug.Log("Loading shop");
        Application.LoadLevel("Shop");
    }
}
