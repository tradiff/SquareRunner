using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    private bool musicEnabled = true;
    private bool soundEnabled = true;
    private Text btnMusicToggleLabel;
    private Text btnSoundToggleLabel;
    private Vector3 scale;
    
    void Awake()
    {
        musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        soundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        btnMusicToggleLabel = transform.Find("btnMusicToggle").GetComponentInChildren<Text>();
        btnSoundToggleLabel = transform.Find("btnSoundToggle").GetComponentInChildren<Text>();
        btnMusicToggleLabel.text = musicEnabled ? "enabled" : "disabled";
        btnSoundToggleLabel.text = soundEnabled ? "enabled" : "disabled";
    }

    public void MusicToggleClick()
    {
        musicEnabled = !musicEnabled;
        PlayerPrefs.SetInt("MusicEnabled", musicEnabled ? 1 : 0);
        SoundManager.Instance.UpdateMusic();
        btnMusicToggleLabel.text = musicEnabled ? "enabled" : "disabled";
    }
    public void SoundToggleClick()
    {
        soundEnabled = !soundEnabled;
        PlayerPrefs.SetInt("SoundEnabled", soundEnabled ? 1 : 0);
        btnSoundToggleLabel.text = soundEnabled ? "enabled" : "disabled";
    }
    public void CloseClick()
    {
        GameManager.Instance.ChangeState(GameManager.GameStates.Playing);
    }


    //void OnGUI()
    //{
    //    var svMat = GUI.matrix; // save current matrix
    //    try
    //    {
    //        scale.x = Screen.width / 640f; // calculate hor scale
    //        scale.y = Screen.height / 400f; // calculate vert scale
    //        scale.z = 1;
    //        // substitute matrix - only scale is altered from standard
    //        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);


    //        if (musicEnabled)
    //            GUI.backgroundColor = Color.green;
    //        else
    //            GUI.backgroundColor = Color.red;

    //        if (GUI.Button(new Rect(210, 210, 70, 30), musicEnabled ? "enabled" : "disabled"))
    //        {
    //            musicEnabled = !musicEnabled;
    //            PlayerPrefs.SetInt("MusicEnabled", musicEnabled ? 1 : 0);
    //            SoundManager.Instance.UpdateMusic();
    //        }

    //        if (soundEnabled)
    //            GUI.backgroundColor = Color.green;
    //        else
    //            GUI.backgroundColor = Color.red;

    //        if (GUI.Button(new Rect(340, 210, 70, 30), soundEnabled ? "enabled" : "disabled"))
    //        {
    //            soundEnabled = !soundEnabled;
    //            PlayerPrefs.SetInt("SoundEnabled", soundEnabled ? 1 : 0);
    //        }


    //        GUI.backgroundColor = Color.white;
    //        if (GUI.Button(new Rect(285, 260, 70, 30), "close"))
    //            GameManager.Instance.ChangeState(GameManager.GameStates.Playing);


    //    }
    //    finally
    //    {
    //        // restore matrix before returning
    //        GUI.matrix = svMat; // restore matrix
    //    }

    //}

}
