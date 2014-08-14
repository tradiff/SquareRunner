using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]  
public class LevelRecapScreen : MonoBehaviour
{
    TypogenicText distanceText;

    void Awake()
    {
        distanceText = transform.Find("Distance").GetComponent<TypogenicText>();

    }

    void Update()
    {
        if (GameManager.Instance.Player.IsDead)
        {
            distanceText.Text = string.Format("{0:N0}m", GameManager.Instance.distanceTraveled);
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(150, 240, 70, 30), "quit"))
            GameManager.Instance.ChangeState(GameManager.GameStates.StartScreen);

        if (GUI.Button(new Rect(310, 240, 70, 30), "retry"))
            GameManager.Instance.ResetGame();
    }
}
