using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelRecapScreen : MonoBehaviour
{
    bool _activated;
    Text _distanceText;
    Text _bankText;
    int _bankAmount;
    int _targetBankAmount;
    int _bankCounterIncrement;

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
            if (_bankAmount < _targetBankAmount)
            {
                _bankAmount += Mathf.Max((int)((float)_bankCounterIncrement * Timekeeper.Instance.deltaTime), 1);
                _bankAmount = Mathf.Min(_bankAmount, _targetBankAmount);
                _bankText.text = string.Format("{0:N0}", _bankAmount);
            }
        }
    }

    public void Activate(bool active, int oldBankAmount)
    {
        if (active)
        {
            var newCoins = GameManager.Instance.coins;
            _bankAmount = oldBankAmount;
            _targetBankAmount = _bankAmount + newCoins;
            _bankCounterIncrement = newCoins / 5; // coins per second
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
