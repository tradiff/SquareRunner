using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private CharacterController2D _controller;
    private float MaxSpeed = 5;
    public bool Activated = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        if (player.IsBig)
        {
            player.IsBig = false;
        }
        else
        {
            player.IsDead = true;
        }
    }
    // Use this for initialization
    void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);

    }

    // Update is called once per frame
    void Update()
    {
        if (Activated)
        {
            _controller.SetHorizontalForce(-MaxSpeed);
        }
        else
        {
            if (Camera.main.transform.position.x > this.transform.position.x - 10)
            {
                Activated = true;
            }
        }

    }
}
