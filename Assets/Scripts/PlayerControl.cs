using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    private bool jump = false; // Condition for whether the player should jump.


    private float moveForce = 365f; // Amount of force added to move the player left and right.
    private float maxSpeed = 5f; // The fastest the player can travel in the x axis.
    public AudioClip[] jumpClips; // Array of clips for when the player jumps.
    private float jumpForce = 400f; // Amount of force added when the player jumps.

    private Transform groundCheck; // A position marking where to check if the player is grounded.
    private bool grounded = false; // Whether or not the player is grounded.

    private bool onGround = false;
    private bool jumped = false;
    private float maxJumpTime = 5f;
    private float jumpTime = 5f;
    private float jumpConstant = 5f;
    private bool released = false;

    void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        //anim = GetComponent<Animator>();
    }

    void Update()
    {
    }


    //void Update()
    //{
    //    // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
    //    grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        Debug.Log("jump");
    //    }

    //    // If the jump button is pressed and the player is grounded then the player should jump.
    //    if (Input.GetButtonDown("Jump") && grounded)
    //        jump = true;
    //}


    //void FixedUpdate()
    //{
    //    // If the player should jump...
    //    if (jump)
    //    {
    //        // Add a vertical force to the player.
    //        rigidbody2D.AddForce(new Vector2(0f, jumpForce));

    //        // Make sure the player can't jump again until the jump conditions from Update are satisfied.
    //        jump = false;
    //    }
    //}


}
