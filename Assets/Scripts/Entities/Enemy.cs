using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public bool Activated = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player collided with enemy");
            var player = other.GetComponent<Hero>();
            if (player.HasHat)
            {
                player.TakeHat();
            }
            else
            {
                player.IsDead = true;
            }
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
