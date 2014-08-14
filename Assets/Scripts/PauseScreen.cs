using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]  
public class PauseScreen : MonoBehaviour
{
    void Awake()
    {

    }

    void Update()
    {
    }


    void OnGUI()
    {
        if (GUI.Button(new Rect(150, 240, 70, 30), "quit"))
            GameManager.Instance.ChangeState(GameManager.GameStates.StartScreen);
        if (GUI.Button(new Rect(230, 240, 70, 30), "settings"))
            GameManager.Instance.ChangeState(GameManager.GameStates.SetttingsScreen);
        if (GUI.Button(new Rect(310, 240, 70, 30), "resume"))
            GameManager.Instance.ChangeState(GameManager.GameStates.Playing);
    }
}
