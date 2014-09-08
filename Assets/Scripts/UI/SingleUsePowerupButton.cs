using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SingleUsePowerupButton : MonoBehaviour
{
    public InventoryItem InventoryItem;

    void Awake()
    {
        UpdateLabels();
    }

    public void UpdateLabels()
    {
        if (InventoryItem == null)
            return;

        if (!string.IsNullOrEmpty(InventoryItem.Image))
            transform.Find("imgForeground").GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/" + InventoryItem.Image);
    }

    public void ButtonClick()
    {
        bool decrease = false;
        if (InventoryItem.SingleUseOwned <= 0)
            return;

        if (InventoryItem.Key == "item_Hat")
        {
            if (!GameManager.Instance.Player.HasHat)
            {
                GameManager.Instance.Player.HasHat = true;
                decrease = true;
            }
        }

        if (InventoryItem.Key == "item_CoinMultiplier")
        {
            if (!GameManager.Instance.HasCoinMultiplier)
            {
                GameManager.Instance.GetCoinMultiplier();
                decrease = true;
            }
        }

        if (InventoryItem.Key == "item_CoinMagnet")
        {
            if (!GameManager.Instance.Player.HasMagnet)
            {
                GameManager.Instance.Player.GetMagnet();
                decrease = true;
            }
        }

        if (decrease)
        {
            InventorySystem.Instance.UpdateQuantity(InventoryItem, InventoryItem.SingleUseOwned - 1);
            GameHud.Instance.usedPowerUpsThisRound.Add(InventoryItem);
        }
        UpdateLabels();
        var _animator = GetComponentInChildren<Animator>();
        _animator.SetTrigger("Pressed");
    }


    public void ButtonClickFinished()
    {
        Debug.Log("ButtonClickFinished");
        GameHud.Instance.UpdatePowerupButtons();
    }
}
