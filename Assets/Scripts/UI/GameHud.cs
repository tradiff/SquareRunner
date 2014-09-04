using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameHud : MonoBehaviour
{
    Text distanceText;
    Text coinsText;
    public CanvasGroup pauseScreen;
    public CanvasGroup settingsScreen;
    public CanvasGroup levelRecapScreen;
    private Object _powerupButtonPrefab;
    private float _itemTop = 0;
    public List<InventoryItem> usedPowerUpsThisRound = new List<InventoryItem>();
    private List<GameObject> powerupButtons = new List<GameObject>();
    public static GameHud Instance
    {
        get
        {
            return _instance;
        }
    }
    private static GameHud _instance;

    public void Awake()
    {
        _instance = this;
        _powerupButtonPrefab = Resources.Load("SingleUsePowerupButton_Prefab");
        distanceText = transform.Find("HUD").Find("Distance").GetComponent<Text>();
        coinsText = transform.Find("HUD").Find("Coins").GetComponent<Text>();
        pauseScreen = transform.Find("PauseScreen").GetComponent<CanvasGroup>();
        settingsScreen = transform.Find("SettingsScreen").GetComponent<CanvasGroup>();
        levelRecapScreen = transform.Find("LevelRecapScreen").GetComponent<CanvasGroup>();

    }

    public void Start()
    {
        ClearPowerupButtons();
    }

    public void Update()
    {
        distanceText.text = string.Format("{0:N0}m", GameManager.Instance.distanceTraveled);
        coinsText.text = "Coins: " + GameManager.Instance.coins;
    }



    public void UpdatePowerupButtons()
    {
        _itemTop = -50;
        foreach (var go in powerupButtons)
        {
            Destroy(go);
        }
        powerupButtons.Clear();
        foreach (var item in InventorySystem.Instance.Items)
        {
            if (!usedPowerUpsThisRound.Exists(x => x.Key == item.Key) && item.ShopItemType == InventoryItem.InventoryItemType.SingleUse && item.SingleUseOwned > 0)
                powerupButtons.Add(CreatePowerupButton(item).gameObject);
        }
    }

    public void ClearPowerupButtons()
    {
        usedPowerUpsThisRound.Clear();
    }

    public void PauseClick()
    {
        GameManager.Instance.ChangeState(GameManager.GameStates.Paused);
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            if (GameManager.Instance.GameState == GameManager.GameStates.Playing)
                GameManager.Instance.ChangeState(GameManager.GameStates.Paused);
    }

    private SingleUsePowerupButton CreatePowerupButton(InventoryItem item)
    {
        var go = (GameObject)Instantiate(_powerupButtonPrefab);
        go.transform.SetParent(transform.FindChild("HUD").transform, false);
        go.GetComponent<RectTransform>().anchoredPosition = new Vector2(10, _itemTop);

        var component = go.GetComponent<SingleUsePowerupButton>();
        component.InventoryItem = item;
        component.UpdateLabels();

        _itemTop -= 58;
        return component;
    }
}
