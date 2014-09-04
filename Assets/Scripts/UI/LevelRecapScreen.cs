using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelRecapScreen : MonoBehaviour
{
    bool _activated;
    Text _distanceText;
    Text _bankText;
    int _bankAmount;
    int _coinCounter;

    void Awake()
    {
        _distanceText = transform.Find("Distance").GetComponent<Text>();
        _bankText = transform.Find("Bank").GetComponent<Text>();
        _activated = false;
    }

    void Update()
    {
        if (_activated)
        {
            _distanceText.text = string.Format("{0:N0}m", GameManager.Instance.distanceTraveled);
            if (_coinCounter > 0)
            {
                _bankAmount += 1;
                _coinCounter -= 1;
                _bankText.text = string.Format("{0:N0}", _bankAmount);
            }
        }
    }

    public void Activate(bool active, int oldBankAmount)
    {
        if (active)
        {
            _bankAmount = oldBankAmount;
            _coinCounter = GameManager.Instance.coins;
        }
        _activated = active;

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
