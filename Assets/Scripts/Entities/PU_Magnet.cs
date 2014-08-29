using UnityEngine;
using System.Collections;

public class PU_Magnet : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Hero>();
        if (player == null)
            return;

        Debug.Log("PU_Magnet collide");
        player.GetMagnet();
        Destroy(gameObject);
    }
}
