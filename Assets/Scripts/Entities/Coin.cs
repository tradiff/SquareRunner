using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collide ");

        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        GameManager.Instance.coins++;
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
