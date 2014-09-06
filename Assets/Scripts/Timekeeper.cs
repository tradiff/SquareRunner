using UnityEngine;
using System.Collections;

public class Timekeeper : MonoBehaviour
{
    public static Timekeeper Instance
    {
        get
        {
            return _instance;
        }
    }
    private static Timekeeper _instance;

    private float prevRealTime;
    private float thisRealTime;

    void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        prevRealTime = thisRealTime;
        thisRealTime = Time.realtimeSinceStartup;
    }

    public float deltaTime
    {
        get
        {
            if (Time.timeScale > 0f) return Time.deltaTime / Time.timeScale;
            return Time.realtimeSinceStartup - prevRealTime; // Checks realtimeSinceStartup again because it may have changed since Update was called
        }
    }

}
