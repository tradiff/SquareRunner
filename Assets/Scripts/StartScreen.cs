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
        if (startJumpKey())
        {
            Debug.Log("Loading level");
            Application.LoadLevel("Level");
        }
    }

    private bool startJumpKey()
    {
        return Input.GetKeyDown(KeyCode.Space) || startTouch();
    }

    private bool startTouch()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }
}
