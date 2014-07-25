using UnityEngine;
using System.Collections;

public class WorldChunk : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - .3f, this.gameObject.transform.position.y, 0);

    }
}
