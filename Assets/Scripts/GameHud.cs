using UnityEngine;
using System.Collections;

public class GameHud : MonoBehaviour
{
    TypogenicText distanceText;
    TypogenicText coinsText;
    GameObject pauseButton;
    Camera hudCamera;

    public void Start()
    {
        distanceText = transform.Find("Distance").GetComponent<TypogenicText>();
        coinsText = transform.Find("Coins").GetComponent<TypogenicText>();
        pauseButton = transform.Find("PauseButton").gameObject;
        hudCamera = GameObject.Find("HUD Camera").GetComponent<Camera>();
    }

    public void Update()
    {
        distanceText.Text = string.Format("{0:N0}m", GameManager.Instance.distanceTraveled);
        coinsText.Text = "Coins: " + GameManager.Instance.coins;
        if (InputManager.Instance.StartTouch())
        {
            var screenPos = InputManager.Instance.GetTouch();
            Vector3 wp = hudCamera.ScreenToWorldPoint(screenPos);
            Vector2 touchPos = new Vector2(wp.x, wp.y);

            Debug.Log("touch: " + touchPos + " pause bounds: " + pauseButton.renderer.bounds);
            Collider2D collider2d = Physics2D.OverlapPoint(touchPos);
            if (collider2d == pauseButton.collider2D)
            {
                GameManager.Instance.PauseGame(true);
            }

            //Ray ray = hudCamera.ScreenPointToRay(touchPosition.Value);
            //RaycastHit hit = new RaycastHit();
            //if (Physics.Raycast(ray, out hit))
            //{
            //    Debug.Log("we hit something");
            //    var go = hit.transform.gameObject;
            //    Debug.Log("Touch Detected on : " + go.name);
            //}



            //if (pauseButton.renderer.bounds.Contains((Vector2)touch))
            //{
            //    Debug.Log("Pause button");

            //}
        }


    }

    //public void OnGUI()
    //{
    //    GUI.skin = Skin;

    //    GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
    //    {
    //        GUILayout.BeginVertical(Skin.GetStyle("GameHud"));
    //        {
    //            GUILayout.Label("Distance: " + GameManager.Instance.distanceTraveled, Skin.GetStyle("DistanceText"));
    //        }
    //        GUILayout.EndVertical();
    //        GUILayout.BeginVertical(Skin.GetStyle("GameHud"));
    //        {
    //            GUILayout.Label("Coins: " + GameManager.Instance.coins, Skin.GetStyle("CoinText"));
    //        }
    //        GUILayout.EndVertical();
    //    }
    //    GUILayout.EndArea();

    //}
}
