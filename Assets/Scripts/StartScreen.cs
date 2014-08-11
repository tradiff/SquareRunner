using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.StartTouch())
        {
            Debug.Log("Loading level");
            Application.LoadLevel("Level");
        }
    }
}
