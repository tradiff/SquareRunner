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
    public bool HasMagnet = false;
    public bool IsDead = false;
    private bool holdingJump = false;
    private int jumpTime = 0;
    private int maxJumpTime = 10;
    private GameObject _hatGO;
    private GameObject _magnetGO;
    public Vector3 lastPosition;
    private float _magnetRange = 5f;
    private float magnetTime = 0;
    private float maxMagnetTime = 30f;


    public void Awake()
    {
        _controller = GetComponent<CharacterController2D>();
        _animator = GetComponentInChildren<Animator>();
        _hatGO = transform.FindChild("HeroSprite/hat").gameObject;
        _magnetGO = transform.FindChild("HeroSprite/magnet").gameObject;
        _isFacingRight = transform.localScale.x > 0;
    }

    public void Update()
    {
        HandleInput();

        _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * SpeedAccellerationOnGround));

        _animator.SetBool("IsGrounded", _controller.State.IsGrounded);
        _animator.SetBool("IsFalling", _controller.Velocity.y < 0);
        _hatGO.SetActive(HasHat);
        _magnetGO.SetActive(HasMagnet);

        var distanceMultiplier = 1;
        if (GameManager.Instance.Area == GameManager.Areas.Bonus)
            distanceMultiplier = 2;
        GameManager.Instance.distanceTraveled += (transform.position.x - lastPosition.x) * distanceMultiplier;
        lastPosition = transform.position;

        if (IsDead && GameManager.Instance.GameState != GameManager.GameStates.RecapScreen)
        {
            GameManager.Instance.ChangeState(GameManager.GameStates.RecapScreen);
        }


        if (HasMagnet)
        {
            if ((magnetTime-= Time.deltaTime) > 0)
            {
                var coins = Physics2D.OverlapCircleAll(transform.position, _magnetRange, 1 << 12);
                foreach (var coin in coins)
                {
                    var tfCoin = coin.transform;
                    var dist = Vector3.Distance(tfCoin.position, transform.position);
                    if (dist <= _magnetRange)
                    {
                        // inside range: attract it
                        var vel = 30 / dist; // velocity inversely proportional to distance
                        // move coin to the player
                        tfCoin.position = Vector3.MoveTowards(tfCoin.position, transform.position, vel * Time.deltaTime);
                    }
                }
            }
            else
            {
                HasMagnet = false;
            }
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


        bool jumpKey = false;
        if (InputManager.Instance.Touching())
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumpKey = true;
            }
            else
            {
                var screenPos = InputManager.Instance.GetTouch();
                Vector3 wp = GameManager.Instance.HudCamera.ScreenToWorldPoint(screenPos);
                Vector2 touchPos = new Vector2(wp.x, wp.y);

                Collider2D collider2d = Physics2D.OverlapPoint(touchPos);
                if (collider2d == GameManager.Instance.TouchTarget.collider2D)
                {
                    jumpKey = true;
                }
            }
        }

        if (holdingJump && jumpKey && jumpTime-- > 0)
        {
            _controller.Jump();
        }
        else
        {
            holdingJump = false;
        }

        if (_controller.State.IsGrounded && InputManager.Instance.StartTouch() && jumpKey)
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

    public void GetMagnet()
    {
        HasMagnet = true;
        magnetTime = maxMagnetTime;
    }
}
