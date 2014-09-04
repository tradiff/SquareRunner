using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    private SpriteRenderer sr;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (GameManager.Instance.HasCoinMultiplier)
                GameManager.Instance.coins+=2;
            else
                GameManager.Instance.coins++;
            SoundManager.Instance.PlaySound(SoundManager.Sounds.Coin);
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Awake()
    {
        sr = this.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMultiplier();
        

    }

    public void UpdateMultiplier()
    {
        if (GameManager.Instance.HasCoinMultiplier)
        {
            sr.color = new Color(1, .5f, 0, 1);
        }
        else
        {
            sr.color = new Color(1, 1, 0, 1);
        }
    }
}
