using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance
    {
        get
        {
            return _instance;
        }
    }
    private static InventorySystem _instance;
    public List<InventoryItem> Items;
    public int Coins
    {
        get
        {
            return PlayerPrefs.GetInt("Coins", 0);
        }
        set
        {
            PlayerPrefs.SetInt("Coins", value);
        }
    }

    void Awake()
    {
        _instance = this;
        UpdateItems();
    }

    private void UpdateItems()
    {
        Items = new List<InventoryItem>();
        Items.Add(new InventoryItem { Key = "item_Hat", Name = "Hat", Description = "Keep an extra hat in your back pocket", Cost = 500, ShopItemType = InventoryItem.InventoryItemType.SingleUse, Image = "single_hat", Max = 3 });
        Items.Add(new InventoryItem { Key = "item_CoinMultiplier", Name = "Coin Multiplier", Description = "2x Coins for 30 seconds", Cost = 1, ShopItemType = InventoryItem.InventoryItemType.SingleUse, Image = "single_coin_multiplier", Max = 3 });
        Items.Add(new InventoryItem { Key = "item_CoinMagnet", Name = "Coin Magnet", Description = "On-demand coin magnet", Cost = 500, ShopItemType = InventoryItem.InventoryItemType.SingleUse, Image = "single_coin_multiplier", Max = 3 });
        Items.Add(new InventoryItem { Key = "item_Respawn", Name = "Respawn", Description = "Opportunity to respawn after dying", Cost = 10000, ShopItemType = InventoryItem.InventoryItemType.SingleUse, Image = "", Max = 3 });
        Items.Add(new InventoryItem { Key = "item_SlowMo", Name = "Slow Mo", Description = "Slow Motion for 10 seconds", Cost = 10000, ShopItemType = InventoryItem.InventoryItemType.SingleUse, Image = "", Max = 3 });
        Items.Add(new InventoryItem { Key = "item_LuckyCharm", Name = "Lucky Charm", Description = "Increases odds of rolling powerups for one run", Cost = 10000, ShopItemType = InventoryItem.InventoryItemType.SingleUse, Image = "", Max = 3 });
        Items.Add(new InventoryItem { Key = "item_DoubleJump", Name = "Double Jump", Description = "Double Jump for one run", Cost = 10000, ShopItemType = InventoryItem.InventoryItemType.SingleUse, Image = "" });

        Items.Add(new InventoryItem { Key = "upgrade_CoinMagnetDuration", Name = "Coin Magnet Duration", Description = "", Cost = 10000, ShopItemType = InventoryItem.InventoryItemType.Upgrade, Image = "" });
        Items.Add(new InventoryItem { Key = "upgrade_CoinMagnetStrength", Name = "Coin Magnet Strength", Description = "", Cost = 10000, ShopItemType = InventoryItem.InventoryItemType.Upgrade, Image = "" });

        foreach (var item in Items)
        {
            if (item.ShopItemType == InventoryItem.InventoryItemType.SingleUse)
                item.SingleUseOwned = PlayerPrefs.GetInt(item.Key, 0);
            if (item.ShopItemType == InventoryItem.InventoryItemType.Upgrade)
                item.UpgradeLevel = PlayerPrefs.GetInt(item.Key, 0);
        }
    }

    public void UpdateQuantity(InventoryItem item, int newQuantity)
    {
        PlayerPrefs.SetInt(item.Key, newQuantity);
        item.SingleUseOwned = PlayerPrefs.GetInt(item.Key, 0);
    }


}
