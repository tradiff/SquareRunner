using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelRecapScreen : MonoBehaviour
{
    Text distanceText;

    void Awake()
    {
        distanceText = transform.Find("Distance").GetComponent<Text>();
    }

    void Update()
    {
        if (GameManager.Instance.Player.IsDead)
        {
            distanceText.text = string.Format("{0:N0}m", GameManager.Instance.distanceTraveled);
        }
    }
    
    public void QuitClick()
    {
        GameManager.Instance.ChangeState(GameManager.GameStates.StartScreen);
    }

    public void RetryClick()
    {
        GameManager.Instance.ResetGame();
    }

}
