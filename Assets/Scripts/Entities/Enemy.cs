using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public bool Activated = false;

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
