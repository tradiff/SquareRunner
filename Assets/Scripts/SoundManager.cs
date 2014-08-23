using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    private AudioClip StartSound;
    private AudioClip JumpSound;
    private AudioClip DieSound;
    private AudioClip CoinSound;
    private AudioClip SpeedIncreaseSound;

    private GameObject musicGO;

    public static SoundManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private static SoundManager _instance;

    void Awake()
    {
        _instance = this;
        StartSound = Resources.Load("sounds/Intruder-Darren_E-7448_hifi") as AudioClip;
        JumpSound = Resources.Load("sounds/PulseSho-Mark_E_B-8071_hifi") as AudioClip;
        DieSound = Resources.Load("sounds/Tweedle_-Intermed-609_hifi") as AudioClip;
        CoinSound = Resources.Load("sounds/Arcade_S-wwwbeat-8529_hifi") as AudioClip;
        SpeedIncreaseSound = Resources.Load("sounds/power_up-Public_D-408_hifi") as AudioClip;

        musicGO = GameObject.Find("Music");
        UpdateMusic();
    }

    void Update()
    {

    }

    public void PlaySound(Sounds sound)
    {
        AudioClip audioClip = null;
        switch (sound)
        {
            case Sounds.Start:
                audioClip = StartSound;
                break;
            case Sounds.Jump:
                audioClip = JumpSound;
                break;
            case Sounds.Die:
                audioClip = DieSound;
                break;
            case Sounds.Coin:
                audioClip = CoinSound;
                break;
            case Sounds.SpeedIncrease:
                audioClip = SpeedIncreaseSound;
                break;
        }

        if (PlayerPrefs.GetInt("SoundEnabled", 1) == 1)
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }


    public void UpdateMusic()
    {
        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
        {
            musicGO.SetActive(true);
        }
        else
        {
            musicGO.SetActive(false);
        }
    }

    public enum Sounds
    {
        Start,
        Jump,
        Die,
        Coin,
        SpeedIncrease
    }
}
