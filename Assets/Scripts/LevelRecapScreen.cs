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

            if (startJumpKey())
            {
                GameManager.Instance.ResetGame();
            }
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
