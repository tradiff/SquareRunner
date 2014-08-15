using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        player.IsDead = true;
    }
}
