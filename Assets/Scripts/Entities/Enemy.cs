using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public bool Activated = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        if (player.HasHat)
        {
            player.HasHat = false;
        }
        else
        {
            player.IsDead = true;
        }
    }

    void Start()
    {

    }

    public void Update()
    {
        if (!Activated && Camera.main.transform.position.x > this.transform.position.x - 15)
        {
            Activated = true;
        }
    }


}
