using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelRecapScreen : MonoBehaviour
{
    const float COUNT_TIME = 3f; // seconds

    bool _activated;
    Text _distanceText;
    Text _bankText;
    int _bankAmount;
    int _targetBankAmount;
    float _bankCounterIncrement;

    int _distance;
    int _targetDistance;
    float _distanceCounterIncrement;

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
            if (_distance < _targetDistance)
            {
                _distance += Mathf.Max((int)((float)_distanceCounterIncrement * Timekeeper.Instance.deltaTime), 1);
                _distance = Mathf.Min(_distance, _targetDistance);
                _distanceText.text = string.Format("{0:N0}m", _distance);
            }

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
            _distance = 0;
            _targetDistance = (int)GameManager.Instance.distanceTraveled;
            _distanceCounterIncrement = _targetDistance / COUNT_TIME;

            var newCoins = GameManager.Instance.coins;
            _bankAmount = oldBankAmount;
            _targetBankAmount = _bankAmount + newCoins;
            _bankCounterIncrement = newCoins / COUNT_TIME; // coins per second
        }
        _activated = active;

    }

    public void QuitClick()
    {
        GameManager.Instance.ChangeState(GameManager.GameStates.StartScreen);
    }
    public void ShopClick()
    {
        GameManager.Instance.ChangeState(GameManager.GameStates.ShopScreen);
    }

    public void RetryClick()
    {
        GameManager.Instance.ResetGame();
    }

}
