using UnityEngine;
using System.Collections;

public class GameHud : MonoBehaviour
{
    public GUISkin Skin;

    public void Start()
    {
        //Skin = (GUISkin)Resources.Load("GameSkin");
    }

    public void OnGUI()
    {
        GUI.skin = Skin;

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        {
            GUILayout.BeginVertical(Skin.GetStyle("GameHud"));
            {
                GUILayout.Label("Distance: " + GameManager.Instance.distanceTraveled, Skin.GetStyle("DistanceText"));
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndArea();

    }
}
