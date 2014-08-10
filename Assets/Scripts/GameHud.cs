using UnityEngine;
using System.Collections;

public class GameHud : MonoBehaviour
{
    TypogenicText distanceText;
    TypogenicText coinsText;

    public void Start()
    {
        distanceText = transform.Find("Distance").GetComponent<TypogenicText>();
        coinsText = transform.Find("Coins").GetComponent<TypogenicText>();
    }

    public void Update()
    {
        distanceText.Text = string.Format("{0:N0}m", GameManager.Instance.distanceTraveled);
        coinsText.Text = "Coins: " + GameManager.Instance.coins;


    }

    //public void OnGUI()
    //{
    //    GUI.skin = Skin;

    //    GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
    //    {
    //        GUILayout.BeginVertical(Skin.GetStyle("GameHud"));
    //        {
    //            GUILayout.Label("Distance: " + GameManager.Instance.distanceTraveled, Skin.GetStyle("DistanceText"));
    //        }
    //        GUILayout.EndVertical();
    //        GUILayout.BeginVertical(Skin.GetStyle("GameHud"));
    //        {
    //            GUILayout.Label("Coins: " + GameManager.Instance.coins, Skin.GetStyle("CoinText"));
    //        }
    //        GUILayout.EndVertical();
    //    }
    //    GUILayout.EndArea();

    //}
}
