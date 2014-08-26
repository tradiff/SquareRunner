using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameHud : MonoBehaviour
{
    Text distanceText;
    Text coinsText;
    public CanvasGroup pauseScreen;
    public CanvasGroup settingsScreen;
    public CanvasGroup levelRecapScreen;

    public void Start()
    {
        GameManager.Instance.GameHud = this;
        distanceText = transform.Find("HUD").Find("Distance").GetComponent<Text>();
        coinsText = transform.Find("HUD").Find("Coins").GetComponent<Text>();
        pauseScreen = transform.Find("PauseScreen").GetComponent<CanvasGroup>();
        settingsScreen = transform.Find("SettingsScreen").GetComponent<CanvasGroup>();
        levelRecapScreen = transform.Find("LevelRecapScreen").GetComponent<CanvasGroup>();
    }

    public void Update()
    {
        distanceText.text = string.Format("{0:N0}m  Speed: {1:N2}x", GameManager.Instance.distanceTraveled, GameManager.Instance.speed);
        coinsText.text = "Coins: " + GameManager.Instance.coins;
    }

    public void PauseClick()
    {
        GameManager.Instance.ChangeState(GameManager.GameStates.Paused);
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            if (GameManager.Instance.GameState == GameManager.GameStates.Playing)
                GameManager.Instance.ChangeState(GameManager.GameStates.Paused);
    }

}
