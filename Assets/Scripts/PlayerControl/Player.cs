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
    public bool IsDead = false;


    public void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _animator = GetComponent<Animator>();
        _isFacingRight = transform.localScale.x > 0;
    }

    public void Update()
    {
        HandleInput();

        var movementFactor = _controller.State.IsGrounded ? SpeedAccellerationOnGround : SpeedAccellerationInAir;
        _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));

        _animator.SetBool("IsGrounded", _controller.State.IsGrounded);
        _animator.SetBool("IsFalling", _controller.Velocity.y < 0);
        _animator.SetBool("IsBig", IsBig);

        GameManager.Instance.distanceTraveled = transform.position.x;

        if (IsDead)
        {
            Debug.Break();
        }
    }

    private void HandleInput()
    {
        // always be running
        _normalizedHorizontalSpeed = 1;

        if (_controller.CanJump && 
            (Input.GetKeyDown(KeyCode.Space) || BeganTouch()))
        {
            _controller.Jump();
        }
    }

    private  bool BeganTouch()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }


}
