using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public WorldGenerator WorldGenerator = null;
    public Player Player = null;
    public float distanceTraveled;
    public int coins;
    public GameObject LevelRecapScreen;
    public GameObject PauseScreen;
    public GameObject SettingsScreen;
    public GameStates GameState;

    public AudioClip StartSound;
    public AudioClip JumpSound;
    public AudioClip DieSound;
    public AudioClip CoinSound;
    public Camera HudCamera;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private static GameManager _instance;

    void Awake()
    {
        _instance = this;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        LevelRecapScreen = GameObject.Find("LevelRecap");
        SettingsScreen = GameObject.Find("SettingsScreen");
        HudCamera = GameObject.Find("HUD Camera").GetComponent<Camera>();
        Debug.Log(LevelRecapScreen);
    }



    public void EndGame()
    {
        GameState = GameStates.RecapScreen;
        AudioSource.PlayClipAtPoint(GameManager.Instance.DieSound, transform.position, 50f);
        Player.SetEnabled(false);
        Time.timeScale = 0;
        StartCoroutine(ShowLevelRecapScreen());
    }

    private IEnumerator ShowLevelRecapScreen()
    {
        yield return StartCoroutine(ExtensionMethods.WaitForRealSeconds(1.3f));
        LevelRecapScreen.SetActive(true);
    }


    public void PauseGame(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0;
            GameState = GameStates.Paused;
            PauseScreen.SetActive(true);
            Player.SetEnabled(false);
        }
        else
        {
            Time.timeScale = 1;
            GameState = GameStates.Playing;
            PauseScreen.SetActive(false);
            Player.SetEnabled(true);
        }
    }

    public void ResetGame()
    {
        Debug.Log("Reset");
        Time.timeScale = 0;
        //Debug.Break();
        Debug.Log(Player);
        Debug.Log(Player.transform);
        Player.transform.position = new Vector3(0, 10, 0);
        distanceTraveled = 0;
        coins = 0;


        this.WorldGenerator.ResetGame();
        this.Player.Reset();
        Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y);
        LevelRecapScreen.SetActive(false);
        PauseScreen.SetActive(false);
        GameState = GameStates.Playing;
        Player.SetEnabled(true);

        Time.timeScale = 1;
        AudioSource.PlayClipAtPoint(GameManager.Instance.StartSound, transform.position);
    }

    public enum GameStates
    {
        StartScreen,
        Playing,
        Paused,
        RecapScreen,
        SetttingsScreen
    }
}
