using UnityEngine;
using System.Collections;

public class PauseScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.Paused)
        {
            if (InputManager.Instance.StartTouch())
            {
                Debug.Log("Resuming");
                GameManager.Instance.PauseGame(false);
            }
        }

    }
}
