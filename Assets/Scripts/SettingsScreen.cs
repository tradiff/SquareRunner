using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]  
public class SettingsScreen : MonoBehaviour
{
    private bool musicEnabled = true;
    private bool soundEnabled = true;
    
    void Awake()
    {
        musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        soundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;

    }

    void OnGUI()
    {
        if (musicEnabled)
            GUI.backgroundColor = Color.green;
        else
            GUI.backgroundColor = Color.red;

        if (GUI.Button(new Rect(170, 180, 70, 30), musicEnabled ? "enabled" : "disabled"))
        {
            musicEnabled = !musicEnabled;
            PlayerPrefs.SetInt("MusicEnabled", musicEnabled ? 1 : 0);
            SoundManager.Instance.UpdateMusic();
        }

        if (soundEnabled)
            GUI.backgroundColor = Color.green;
        else
            GUI.backgroundColor = Color.red;

        if (GUI.Button(new Rect(290, 180, 70, 30), soundEnabled ? "enabled" : "disabled"))
        {
            soundEnabled = !soundEnabled;
            PlayerPrefs.SetInt("SoundEnabled", soundEnabled ? 1 : 0);
        }


        GUI.backgroundColor = Color.white;
        if (GUI.Button(new Rect(230, 240, 70, 30), "close"))
            GameManager.Instance.ChangeState(GameManager.GameStates.Playing);
    }

}
