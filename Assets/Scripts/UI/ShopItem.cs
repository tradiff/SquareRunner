using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public InventoryItem InventoryItem;
    
    void Awake()
    {
        UpdateLabels();
        //InventorySystem.Instance.Coins = 100000;
    }

    public void UpdateLabels()
    {
        if (InventoryItem == null)
            return;
        int owned = PlayerPrefs.GetInt(InventoryItem.Key, 0);

        bool canAfford = InventorySystem.Instance.Coins >= InventoryItem.Cost;
        transform.FindChild("Name").GetComponent<Text>().text = InventoryItem.Name;
        transform.FindChild("Description").GetComponent<Text>().text = InventoryItem.Description;
        transform.FindChild("btnBuy/Text").GetComponent<Text>().text = "<color=#ffff00ff>◆</color> " + InventoryItem.Cost.ToString("N0");
        transform.FindChild("btnBuy").GetComponent<Image>().color = canAfford ? new Color(0, 1, 0, .59f) : new Color(1, 1, 1, .59f);
        transform.FindChild("btnBuy/Text").GetComponent<Text>().color = canAfford ? Color.white : Color.red;
        transform.FindChild("Own").GetComponent<Text>().text = String.Format("{0:N0}", owned);

        if (!string.IsNullOrEmpty(InventoryItem.Image))
            transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/" + InventoryItem.Image);
    }

    public void BuyClick()
    {
        if (InventorySystem.Instance.Coins >= InventoryItem.Cost)
        {
            InventorySystem.Instance.Coins -= InventoryItem.Cost;
            if (InventoryItem.ShopItemType == InventoryItem.InventoryItemType.SingleUse)
                InventorySystem.Instance.UpdateQuantity(this.InventoryItem, this.InventoryItem.SingleUseOwned+1);

            if (InventoryItem.ShopItemType == InventoryItem.InventoryItemType.Upgrade)
            {
                // todo: upgrade
            }
        }
        transform.FindChild("Own").GetComponent<Animator>().SetTrigger("TextBump2");
        ShopScreen.Instance.UpdateLabels();
    }

}
