using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Hero>();
        if (player == null)
            return;

        Debug.Log("coin collide");
        GameManager.Instance.coins++;
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Coin);
        Destroy(gameObject);

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
