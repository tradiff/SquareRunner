using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player collided with destroyer");
            other.GetComponent<Player>().IsDead = true;
            return;
        }

        if (other.tag == "WorldChunkPrefab")
        {
            Destroy(other.gameObject);
        }

        if (other.tag == "Entity")
        {
            Destroy(other.gameObject);
        }

        //if (other.gameObject.transform.parent)
        //{
        //    Destroy(other.gameObject.transform.parent.gameObject);
        //}
        //else
        //{
        //    Destroy(other.gameObject);
        //}
    }
}
