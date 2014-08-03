using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player collided with destroyer");
            GameManager.Instance.ResetGame();
            return;
        }

        if (other.tag == "Entity")
        {
            return;
        }

        if (other.gameObject.transform.parent)
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
