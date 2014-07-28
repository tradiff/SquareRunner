using UnityEngine;
using System.Collections;

public class GameManager
{
    public float distanceTraveled;

    public static GameManager Instance
    {
        get
        {
            return _instance ?? (_instance = new GameManager());
        }
    }
    private static GameManager _instance;

    private GameManager() { }

    public void Reset()
    {
        distanceTraveled = 0;

    }
}
