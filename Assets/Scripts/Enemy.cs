using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collide!");
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        if (player.IsBig)
        {
            player.IsBig = false;
        }
        else
        {
            player.IsDead = true;
        }

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
