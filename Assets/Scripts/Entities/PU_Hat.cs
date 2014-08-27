using UnityEngine;
using System.Collections;

public class PU_Hat : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Hero>();
        if (player == null)
            return;

        Debug.Log("PU_Hat collide");
        player.HasHat = true;
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
