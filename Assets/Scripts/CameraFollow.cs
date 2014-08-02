using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    float dampening = 5;

    // Update is called once per frame
    void Update()
    {
        
        var newX = Mathf.Lerp(transform.position.x, player.position.x + 6, dampening * Time.deltaTime);
        transform.position = new Vector3(newX, 5.0f, -10);

        //transform.position = new Vector3(player.position.x + 6, 3.4f, -10);
    }
}
