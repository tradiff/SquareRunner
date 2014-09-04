using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

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
        foreach (var item in InventorySystem.Instance.Items.Where(x => x.ShopItemType == InventoryItem.InventoryItemType.SingleUse))
        {
            CreateShopItem(item);
        }

        CreateShopLabel("Upgrades");
        foreach (var item in InventorySystem.Instance.Items.Where(x => x.ShopItemType == InventoryItem.InventoryItemType.Upgrade))
        {
            CreateShopItem(item);
        }
    }

    private void CreateShopLabel(string name)
    {
        var go = (GameObject)Instantiate(_shopCategoryLabelPrefab);
        go.transform.SetParent(transform.FindChild("ScrollView/container").transform, false);
        go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _itemTop);
        go.GetComponent<Text>().text = name;

        _itemTop -= 70;
    }

    private ShopItem CreateShopItem(InventoryItem item)
    {
        var siGO = (GameObject)Instantiate(_shopItemPrefab);
        siGO.transform.SetParent(transform.FindChild("ScrollView/container").transform, false);
        siGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, _itemTop);

        var si = siGO.GetComponent<ShopItem>();
        si.InventoryItem = item;
        _shopItems.Add(si);
        si.UpdateLabels();

        _itemTop -= 70;
        return si;
    }

    public void CloseClick()
    {
        Application.LoadLevel("StartScreen");
    }

    public enum ShopItemType
    {
        SingleUse,
        Upgrade
    }


}
