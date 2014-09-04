using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public float speed = 1;
    public float distanceTraveled;
    public int coins;
    public GameStates GameState;
    public Areas Area;
    private Vector3 lastNormalPlayPosition;

    public WorldGenerator WorldGenerator = null;
    public Hero Player = null;
    public Camera HudCamera;
    public GooglePlayManager GooglePlayManager;
    public GameObject TouchTarget;
    public bool HasCoinMultiplier = false;
    private float _coinMultiplierTime = 0;
    private float _coinMultiplierMaxTime = 30f;

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
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>();
        HudCamera = GameObject.Find("HUD Camera").GetComponent<Camera>();
        GooglePlayManager = transform.GetComponent<GooglePlayManager>();
        TouchTarget = GameObject.Find("TouchTarget");
    }

    void Update()
    {
        if (HasCoinMultiplier)
        {
            if ((_coinMultiplierTime -= Time.deltaTime) <= 0)
            {
                HasCoinMultiplier = false;
            }
        }
    }

    public void IncreaseSpeed()
    {
        speed += 0.25f;
        SoundManager.Instance.PlaySound(SoundManager.Sounds.SpeedIncrease);
        Player.SpeedUpdated();
        Camera.main.GetComponent<CameraFollow>().StartShake();
    }


    private IEnumerator ShowLevelRecapScreen(int oldBankAmount)
    {
        yield return StartCoroutine(ExtensionMethods.WaitForRealSeconds(1.3f));
        GameHud.Instance.levelRecapScreen.alpha = 1;
        GameHud.Instance.levelRecapScreen.interactable = true;
        GameHud.Instance.levelRecapScreen.blocksRaycasts = true;
        GameHud.Instance.levelRecapScreen.GetComponent<LevelRecapScreen>().Activate(true, oldBankAmount);
    }


    public void ChangeState(GameStates newState)
    {
        // reset
        float newTimeScale = 0;
        Player.SetEnabled(false);
        GameHud.Instance.pauseScreen.alpha = 0;
        GameHud.Instance.pauseScreen.interactable = false;
        GameHud.Instance.pauseScreen.blocksRaycasts = false;
        GameHud.Instance.settingsScreen.alpha = 0;
        GameHud.Instance.settingsScreen.interactable = false;
        GameHud.Instance.settingsScreen.blocksRaycasts = false;
        GameHud.Instance.levelRecapScreen.alpha = 0;
        GameHud.Instance.levelRecapScreen.interactable = false;
        GameHud.Instance.levelRecapScreen.blocksRaycasts = false;
        

        switch (newState)
        {
            case GameStates.StartScreen:
                Application.LoadLevel("StartScreen");
                break;
            case GameStates.Playing:
                GameHud.Instance.UpdatePowerupButtons();
                newTimeScale = 1;
                Player.SetEnabled(true);
                break;
            case GameStates.Paused:
                GameHud.Instance.pauseScreen.alpha = 1;
                GameHud.Instance.pauseScreen.interactable = true;
                GameHud.Instance.pauseScreen.blocksRaycasts = true;
                break;
            case GameStates.RecapScreen:
                SoundManager.Instance.PlaySound(SoundManager.Sounds.Die);
                int oldBankAmount = PlayerPrefs.GetInt("Coins", 0);
                PlayerPrefs.SetInt("Coins", oldBankAmount + coins);
                GooglePlayManager.ReportScore((int)this.distanceTraveled);
                StartCoroutine(ShowLevelRecapScreen(oldBankAmount)); // delay for the sound to finish
                break;
            case GameStates.SetttingsScreen:
                GameHud.Instance.settingsScreen.alpha = 1;
                GameHud.Instance.settingsScreen.interactable = true;
                GameHud.Instance.settingsScreen.blocksRaycasts = true;
                break;
        }
        GameState = newState;
        Time.timeScale = newTimeScale;
    }

    public void ChangeArea(Areas newArea)
    {
        var oldArea = this.Area;
        this.Area = newArea;
        if (newArea == Areas.Bonus)
        {
            WorldGenerator.GenerateBonusChunks();
            lastNormalPlayPosition = Player.transform.position;
            Player.transform.position = new Vector3(0, Player.transform.position.y + 100, Player.transform.position.z);
            Player.lastPosition = Player.transform.position;
            Camera.main.transform.position = Player.transform.position;
        }
        if (newArea == Areas.Normal)
        {
            WorldGenerator.DestroyBonusChunks();
            Player.transform.position = new Vector3(lastNormalPlayPosition.x, 10, Player.transform.position.z);
            Player.lastPosition = Player.transform.position;
            Camera.main.transform.position = Player.transform.position;
        }

    }

    public void ResetGame()
    {
        Debug.Log("Reset");
        Time.timeScale = 0;
        Debug.Log(Player);
        Debug.Log(Player.transform);
        Player.transform.position = new Vector3(0, 10, 0);
        Player.lastPosition = Player.transform.position;
        speed = 1;
        distanceTraveled = 0;
        coins = 0;


        this.WorldGenerator.ResetGame();
        this.Player.Reset();
        this.HasCoinMultiplier = false;
        _coinMultiplierTime = 0;
        GameHud.Instance.ClearPowerupButtons();
        Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y);
        ChangeState(GameStates.Playing);
        Player.SetEnabled(true);
        Player.SpeedUpdated();


        Time.timeScale = 1;
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Start);
    }
    public void GetCoinMultiplier()
    {
        HasCoinMultiplier = true;
        _coinMultiplierTime = _coinMultiplierMaxTime;
    }

    public enum GameStates
    {
        StartScreen,
        Playing,
        Paused,
        RecapScreen,
        SetttingsScreen
    }

    public enum Areas
    {
        Normal,
        Bonus
    }
}
