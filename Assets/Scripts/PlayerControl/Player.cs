using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private bool _isFacingRight;
    private CharacterController2D _controller;
    private float _normalizedHorizontalSpeed;

    private float MaxSpeed = 18;
    private float SpeedAccellerationOnGround = 10f;
    private float SpeedAccellerationInAir = 10f;

    private Animator _animator;

    public bool IsBig = false;

    public void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _animator = GetComponent<Animator>();
        _isFacingRight = transform.localScale.x > 0;
        _animator.SetTrigger("Run");
    }

    public void Update()
    {
        HandleInput();

        var movementFactor = _controller.State.IsGrounded ? SpeedAccellerationOnGround : SpeedAccellerationInAir;
        _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));

        _animator.SetBool("IsGrounded", _controller.State.IsGrounded);
        _animator.SetBool("IsFalling", _controller.Velocity.y < 0);
        _animator.SetBool("IsBig", IsBig);

    }

    private void HandleInput()
    {
        // always be running
        _normalizedHorizontalSpeed = 1;

        //if (Input.GetKey(KeyCode.D))
        //{
        //    _normalizedHorizontalSpeed = 1;
        //    if (!_isFacingRight)
        //        Flip();
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    _normalizedHorizontalSpeed = -1;
        //    if (_isFacingRight)
        //        Flip();
        //}
        //else
        //{
        //    _normalizedHorizontalSpeed = 0;
        //}

        if (_controller.CanJump && Input.GetKeyDown(KeyCode.Space))
        {
            _controller.Jump();
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;
    }


}
