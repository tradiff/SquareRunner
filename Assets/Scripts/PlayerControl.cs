using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;			// For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;				// Condition for whether the player should jump.


    public float moveForce = 365f;			// Amount of force added to move the player left and right.
    public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
    public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
    private float jumpForce = 400f;			// Amount of force added when the player jumps.

    private Transform groundCheck;			// A position marking where to check if the player is grounded.
    private bool grounded = false;			// Whether or not the player is grounded.
    //private Animator anim;					// Reference to the player's animator component.


    void Awake()
    {
        Debug.Log("awake");
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        //anim = GetComponent<Animator>();
    }


    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("jump");
        }

        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
    }


    void FixedUpdate()
    {
        // If the player should jump...
        if (jump)
        {
            // Set the Jump animator trigger parameter.
            //anim.SetTrigger("Jump");

            // Play a random jump audio clip.
            //int i = Random.Range(0, jumpClips.Length);
            //AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            // Add a vertical force to the player.
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
    }


    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}
