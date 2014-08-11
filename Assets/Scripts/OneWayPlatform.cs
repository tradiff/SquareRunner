using UnityEngine;
using System.Collections;

public class OneWayPlatform : MonoBehaviour
{
    const int PLATFORM_LAYER = 8;
    const int DISABLED_PLATFORM_LAYER = 9;

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        transform.parent.gameObject.layer = DISABLED_PLATFORM_LAYER;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        transform.parent.gameObject.layer = PLATFORM_LAYER;
    }
}
