using UnityEngine;
using System.Collections;

public class RedMushroom : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collide ");

        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        player.IsBig = true;
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
