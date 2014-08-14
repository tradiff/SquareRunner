using System;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]  
public class PauseScreen : MonoBehaviour
{
    private Vector3 scale;
    void Awake()
    {

    }

    void Update()
    {
    }


    void OnGUI()
    {
        var svMat = GUI.matrix; // save current matrix
        try
        {
            scale.x = Screen.width / 640f; // calculate hor scale
            scale.y = Screen.height / 400f; // calculate vert scale
            scale.z = 1;
            // substitute matrix - only scale is altered from standard
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);


            if (GUI.Button(new Rect(205, 240, 70, 30), "quit"))
                GameManager.Instance.ChangeState(GameManager.GameStates.StartScreen);
            if (GUI.Button(new Rect(285, 240, 70, 30), "settings"))
                GameManager.Instance.ChangeState(GameManager.GameStates.SetttingsScreen);
            if (GUI.Button(new Rect(365, 240, 70, 30), "resume"))
                GameManager.Instance.ChangeState(GameManager.GameStates.Playing);


        }
        finally
        {
            // restore matrix before returning
            GUI.matrix = svMat; // restore matrix
        }
    }
}
