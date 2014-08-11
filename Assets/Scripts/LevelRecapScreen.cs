using UnityEngine;
using System.Collections;

public class LevelRecapScreen : MonoBehaviour
{
    TypogenicText distanceText;

    void Start()
    {
        distanceText = transform.Find("Distance").GetComponent<TypogenicText>();

    }

    void Update()
    {
        if (GameManager.Instance.Player.IsDead)
        {

            distanceText.Text = string.Format("{0:N0}m", GameManager.Instance.distanceTraveled);

            if (InputManager.Instance.StartTouch())
            {
                GameManager.Instance.ResetGame();
            }
        }
    }
}
