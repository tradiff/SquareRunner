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
        PauseScreen = GameObject.Find("PauseScreen");
        SettingsScreen = GameObject.Find("SettingsScreen");
        HudCamera = GameObject.Find("HUD Camera").GetComponent<Camera>();
        Debug.Log(LevelRecapScreen);
    }

    void Update()
    {
        
    }



    //public void EndGame()
    //{
    //    GameState = GameStates.RecapScreen;
    //    SoundManager.Instance.PlaySound(SoundManager.Sounds.Die);
    //    Player.SetEnabled(false);
    //    Time.timeScale = 0;
    //    StartCoroutine(ShowLevelRecapScreen());
    //}

    private IEnumerator ShowLevelRecapScreen()
    {
        yield return StartCoroutine(ExtensionMethods.WaitForRealSeconds(1.3f));
        LevelRecapScreen.SetActive(true);
    }


    public void ChangeState(GameStates newState)
    {
        // reset
        var newTimeScale = 0;
        Player.SetEnabled(false);
        LevelRecapScreen.SetActive(false);
        PauseScreen.SetActive(false);
        SettingsScreen.SetActive(false);

        switch (newState)
        {
            case GameStates.StartScreen:
                Application.LoadLevel("StartScreen");
                break;
            case GameStates.Playing:
                newTimeScale = 1;
                Player.SetEnabled(true);
                break;
            case GameStates.Paused:
                PauseScreen.SetActive(true);
                break;
            case GameStates.RecapScreen:
                SoundManager.Instance.PlaySound(SoundManager.Sounds.Die);
                StartCoroutine(ShowLevelRecapScreen()); // delay for the sound to finish
                break;
            case GameStates.SetttingsScreen:
                SettingsScreen.SetActive(true);
                break;
        }
        GameState = newState;
        Time.timeScale = newTimeScale;
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
        ChangeState(GameStates.Playing);
        Player.SetEnabled(true);

        Time.timeScale = 1;
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Start);
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
