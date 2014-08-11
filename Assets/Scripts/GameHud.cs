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
        }


    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)

            GameManager.Instance.PauseGame(true);
    }

}
