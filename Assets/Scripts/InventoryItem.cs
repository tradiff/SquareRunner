using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class InventoryItem
{
    public string Key;
    public string Name;
    public string Description;
    public int Cost;
    public InventoryItemType ShopItemType;
    public string Image;
    public int SingleUseOwned;
    public int UpgradeLevel;
    public int Max;

    public enum InventoryItemType
    {
        SingleUse,
        Upgrade
    }
}
