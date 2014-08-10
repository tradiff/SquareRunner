using UnityEngine;
using System.Collections;

public class EnemyVine : Enemy
{
    private CharacterController2D _controller;
    private float MaxSpeed = 5;


    // Use this for initialization
    void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (Activated)
        {

        }

    }
}
