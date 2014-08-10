﻿using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using System.Collections;

public class GameManager
{
    public WorldGenerator WorldGenerator = null;
    public Player Player = null;
    public float distanceTraveled;
    public int coins;
    public GameObject LevelRecapScreen;
    public GameStates GameState;

    public static GameManager Instance
    {
        get
        {
            return _instance ?? (_instance = new GameManager());
        }
    }
    private static GameManager _instance;

    private GameManager()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        LevelRecapScreen = GameObject.Find("LevelRecap");
        Debug.Log(LevelRecapScreen);
        //BackgroundHolderGameObject = GameObject.FindGameObjectWithTag("BackgroundHolder");
    }

    public void EndGame()
    {
        GameState = GameStates.RecapScreen;
        LevelRecapScreen.SetActive(true);
        Player.SetEnabled(false);
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
        GameState = GameStates.Playing;
        Player.SetEnabled(true);

        Time.timeScale = 1;
    }

    public enum GameStates
    {
        StartScreen,
        Playing,
        Paused,
        RecapScreen
    }
}
