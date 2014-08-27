using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{
    private bool _isFacingRight;
    private CharacterController2D _controller;
    private float _normalizedHorizontalSpeed;

    private float MaxSpeed = 10;
    private float SpeedAccellerationOnGround = 10f;
    private Animator _animator;

    public bool HasHat = false;
    public bool IsDead = false;
    private bool holdingJump = false;
    private int jumpTime = 0;
    private int maxJumpTime = 10;
    private GameObject _hatGO;
    public Vector3 lastPosition;


    public void Awake()
    {
        _controller = GetComponent<CharacterController2D>();
        _animator = GetComponentInChildren<Animator>();
        _hatGO = transform.FindChild("HeroSprite/hat").gameObject;

        _isFacingRight = transform.localScale.x > 0;
    }

    public void Update()
    {
        HandleInput();

        _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * SpeedAccellerationOnGround));

        _animator.SetBool("IsGrounded", _controller.State.IsGrounded);
        _animator.SetBool("IsFalling", _controller.Velocity.y < 0);
        _hatGO.SetActive(HasHat);

        var distanceMultiplier = 1;
        if (GameManager.Instance.Area == GameManager.Areas.Bonus)
            distanceMultiplier = 4;
        GameManager.Instance.distanceTraveled += (transform.position.x - lastPosition.x) * distanceMultiplier;
        lastPosition = transform.position;

        if (IsDead && GameManager.Instance.GameState != GameManager.GameStates.RecapScreen)
        {
            GameManager.Instance.ChangeState(GameManager.GameStates.RecapScreen);
        }

    }

    private void HandleInput()
    {
        // always be running
        _normalizedHorizontalSpeed = GameManager.Instance.speed;
        //_normalizedHorizontalSpeed = 0;

        //if (Input.GetKey(KeyCode.A))
        //{
        //    _normalizedHorizontalSpeed = -1;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    _normalizedHorizontalSpeed = 1;
        //}

        if (holdingJump && InputManager.Instance.Touching() && jumpTime-- > 0)
        {
            _controller.Jump();
        }
        else
        {
            holdingJump = false;
        }

        if (_controller.State.IsGrounded && InputManager.Instance.StartTouch())
        {
            _controller.Jump();
            holdingJump = true;
            jumpTime = maxJumpTime;
            SoundManager.Instance.PlaySound(SoundManager.Sounds.Jump);
        }

    }

    public void SetEnabled(bool enabled)
    {
        this.enabled = enabled;
        if (_controller != null)
            _controller.enabled = enabled;
    }

    public void Reset()
    {
        HasHat = false;
        IsDead = false;
    }

    public void HitDestroyer()
    {
        if (GameManager.Instance.Area == GameManager.Areas.Bonus)
        {
            GameManager.Instance.ChangeArea(GameManager.Areas.Normal);
        }
        else
        {
            this.IsDead = true;
        }
    }
}
