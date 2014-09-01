using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour
{
    public static ShopScreen Instance
    {
        get
        {
            return _instance;
        }
    }
    private static ShopScreen _instance;
    private List<ShopItem> _shopItems;
    private Object _shopItemPrefab;
    private Object _shopCategoryLabelPrefab;
    private float _itemTop = 0;

    void Awake()
    {
        _instance = this;
        _shopItemPrefab = Resources.Load("ShopItem_Prefab");
        _shopCategoryLabelPrefab = Resources.Load("ShopCategoryLabel_Prefab");
        UpdateLabels();
        CreateShopItems();
    }

    public void UpdateLabels()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        transform.FindChild("Coins").GetComponent<Text>().text = "Coins: " + coins.ToString("N0");
    }

    private void CreateShopItems()
    {
        _shopItems = new List<ShopItem>();
        _itemTop = -30;

        // idea: add gems in coins, worth 10 coins

        // single use items:
        //     coin multiplier
        //     extra boost at the end
        //     respawn after death
        //     hat
        //     slow mo
        //     lucky
        //     double jump
        //     autopilot

        // upgrades:
        //     coin magnet - increase duration
        //     coin magnet - increase strength

        // just for fun:
        //     rainbow goggles
        //     sumbraro hat
        //     cowboy hat
        //     fart jumps


        CreateShopLabel("Single Use Items");
        CreateShopItem("item_CoinMultiplier", "Coin Multiplier", "2x Coins for 10 seconds", 10000, ShopItemType.SingleUse);
        CreateShopItem("item_Respawn", "Respawn", "Opportunity to respawn after dying", 10000, ShopItemType.SingleUse);
        CreateShopItem("item_Hat", "Hat", "Keep an extra hat in your back pocket", 10000, ShopItemType.SingleUse);
        CreateShopItem("item_SlowMo", "Slow Mo", "Slow Motion for 10 seconds", 10000, ShopItemType.SingleUse);
        CreateShopItem("item_LuckyCharm", "Lucky Charm", "Increased odds of rolling powerups", 10000, ShopItemType.SingleUse);
        CreateShopItem("item_DoubleJump", "Double Jump", "Double Jump for one run", 10000, ShopItemType.SingleUse);

        CreateShopLabel("Upgrades");
        CreateShopItem("upgrade_CoinMagnetDuration", "Coin Magnet Duration", "2x Coins for 10 seconds", 10000, ShopItemType.Upgrade);
        CreateShopItem("upgrade_CoinMagnetStrength", "Coin Magnet Strength", "", 10000, ShopItemType.Upgrade);
    }

    private void CreateShopLabel(string name)
    {
        var go = (GameObject)Instantiate(_shopCategoryLabelPrefab);
        go.transform.SetParent(transform.FindChild("ScrollView/container").transform, false);
        go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _itemTop);
        go.GetComponent<Text>().text = name;

        _itemTop -= 70;
    }

    private ShopItem CreateShopItem(string key, string name, string description, int cost, ShopItemType shopItemType)
    {
        var siGO = (GameObject)Instantiate(_shopItemPrefab);
        siGO.transform.SetParent(transform.FindChild("ScrollView/container").transform, false);
        siGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _itemTop);

        var si = siGO.GetComponent<ShopItem>();
        si.Key = key;
        si.Name = name;
        si.Description = description;
        si.Cost = cost;
        si.ShopItemType = shopItemType;
        _shopItems.Add(si);
        si.UpdateLabels();

        _itemTop -= 70;
        return si;
    }

    public enum ShopItemType
    {
        SingleUse,
        Upgrade
    }


}
