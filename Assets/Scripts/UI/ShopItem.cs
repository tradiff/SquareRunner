using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public string Key;
    public string Name;
    public string Description;
    public int Cost;
    public ShopScreen.ShopItemType ShopItemType;
    
    void Awake()
    {
        UpdateLabels();
    }

    public void UpdateLabels()
    {
        int owned = PlayerPrefs.GetInt(Key, 0);

        transform.FindChild("Name").GetComponent<Text>().text = Name;
        transform.FindChild("Description").GetComponent<Text>().text = Description;
        transform.FindChild("Cost").GetComponent<Text>().text = Cost.ToString("N0");
        transform.FindChild("Own").GetComponent<Text>().text = owned.ToString("N0") + " Owned";
        ShopScreen.Instance.UpdateLabels();
    }

    public void BuyClick()
    {
        int coinsInBank = PlayerPrefs.GetInt("Coins", 0);
        if (coinsInBank > Cost)
        {
            coinsInBank -= Cost;
            PlayerPrefs.SetInt("Coins", coinsInBank);
            int owned = PlayerPrefs.GetInt(Key, 0);
            owned++;
            PlayerPrefs.SetInt(Key, owned);
        }
        UpdateLabels();
    }

}
