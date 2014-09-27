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
    public bool IsGiant = false;
    private float jumpTime = 0;
    private float maxJumpTime = .1666f;
    private GameObject _hatGO;
    private GameObject _magnetGO;
    public Vector3 lastPosition;
    private float _magnetRange = 5f;
    private float magnetTime = 0;
    private float maxMagnetTime = 30f;
    private float giantTime = 0;
    private float maxGiantTime = 30f;
    private GameObject _currentChunk;
    private TrailRenderer _trail;


    public void Awake()
    {
        _controller = GetComponent<CharacterController2D>();
        _animator = GetComponentInChildren<Animator>();
        _hatGO = transform.FindChild("HeroSprite/hat").gameObject;
        _magnetGO = transform.FindChild("HeroSprite/magnet").gameObject;
        _trail = transform.GetComponentInChildren<TrailRenderer>();
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

        if (IsGiant)
        {
            if ((giantTime -= Time.deltaTime) > 0)
            {
                if (transform.localScale.x != 5)
                {
                    transform.localScale = new Vector3(5, 5, 5);
                    _controller.UpdateSize();
                }
            }
            else
            {
                IsGiant = false;
            }
        }
        else
        {
            if (transform.localScale.x != 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
                _controller.UpdateSize();
            }
        }


        if (HasMagnet)
        {
            if ((magnetTime -= Time.deltaTime) > 0)
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
        if (InputManager.Instance.FullScreenTouching())
        {
            jumpKey = true;
        }

        if (holdingJump && jumpKey && (jumpTime -= Time.deltaTime) > 0)
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
        HasMagnet = false;
        IsGiant = false;
    }

    public void SpeedUpdated()
    {
        _trail.material.SetColor("_TintColor", new Color(1f, 0, 0, 1f));
        if (GameManager.Instance.speed == 1.0f)
        {
            _trail.material.SetColor("_TintColor", new Color(1f, 1f, 1f, 1f));
        }
        if (GameManager.Instance.speed == 1.25f)
        {
            _trail.material.SetColor("_TintColor", new Color(1f, 1f, 0f, 1f));
        }
        if (GameManager.Instance.speed == 1.50f)
        {
            _trail.material.SetColor("_TintColor", new Color(1f, 0f, 0f, 1f));
        }
        if (GameManager.Instance.speed == 1.75f)
        {
            _trail.material.SetColor("_TintColor", new Color(1f, 0f, 1f, 1f));
        }
        if (GameManager.Instance.speed == 2.00f)
        {
            _trail.material.SetColor("_TintColor", new Color(0f, 0f, 1f, 1f));
        }
    }

    public void EnterChunk(GameObject chunk)
    {
        _currentChunk = chunk;
        this.transform.FindChild("HeroSprite").GetComponent<SpriteRenderer>().color = chunk.GetComponent<WorldChunk>().Biome.playerColor;
        this.transform.FindChild("HeroSprite").FindChild("hat").GetComponent<SpriteRenderer>().color = chunk.GetComponent<WorldChunk>().Biome.playerColor;
    }

    public void HitDestroyer()
    {
        if (GameManager.Instance.Area == GameManager.Areas.Bonus)
        {
            GameManager.Instance.ChangeArea(GameManager.Areas.Normal);
        }
        else
        {
            if (this.HasHat)
            {
                this.HasHat = false;
                Respawn();

            }
            else
            {
                this.IsDead = true;
            }
        }
    }

    private void Respawn()
    {
        // find nearest spawn point
        var spawns = GameObject.FindGameObjectsWithTag("SpawnTarget");
        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject spawn in spawns)
        {
            Vector3 diff = spawn.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = spawn;
                distance = curDistance;
            }
        }

        transform.position = closest.transform.position;
        _controller.Jump();
    }

    public void GetMagnet()
    {
        HasMagnet = true;
        magnetTime = maxMagnetTime;
    }

    public void GetGiant()
    {
        IsGiant = true;
        giantTime = maxGiantTime;
    }

    public void TakeHat()
    {
        HasHat = false;

        // clear all enemies on screen
        var entities = GameObject.FindGameObjectsWithTag("Entity");
        foreach (var entity in entities)
        {
            if (entity.GetComponent<Enemy>())
            {
                Destroy(entity);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Entity")
        {
            if (IsGiant) return;

            var enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (HasHat)
                {
                    TakeHat();
                }
                else
                {
                    IsDead = true;
                }
            }
        }
    }

}
