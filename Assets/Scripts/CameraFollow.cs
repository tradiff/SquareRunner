using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    float dampening = 5;

    // Update is called once per frame
    void Update()
    {
        
        var newX = Mathf.Lerp(transform.position.x, player.position.x + 10, dampening * Time.deltaTime);
        transform.position = new Vector3(newX, GameManager.Instance.Area == GameManager.Areas.Bonus ? 107f : 7f, -10);

    }
}
