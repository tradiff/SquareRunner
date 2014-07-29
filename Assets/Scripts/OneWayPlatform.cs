using UnityEngine;
using System.Collections;

public class OneWayPlatform : MonoBehaviour
{
    void Start()
    {
        transform.parent.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        transform.parent.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        transform.parent.GetComponent<BoxCollider2D>().enabled = true;
    }
}
