using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour, IDamageable
{
    public static UnityEvent dashEvent = new UnityEvent();
    public static Player instance;
    private PlayerInputHandler _input;
    private Rigidbody2D _rb;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Transform _gun;
    public Vector2 gunDirection;
    [SerializeField] private SpriteRenderer _sp;
    [SerializeField] private SpriteRenderer _spGun;

    [Header("Dash stuff")]
    private bool _isDashing;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashCooldown;
    private float _timeWhenDashed;
    private bool _canDash = true;

    [Header("Damage Stuff")]
    private bool _canBeDamaged = true;
    [SerializeField] private float _iFrameTime = 1;

    [Header("Life stuff")]
    public int maxLife;
    public int currentLife;
    private bool _wasFlipped = true;

    [SerializeField] private Transform _ropeAnchor;

    [SerializeField] private ParticleSystem _psDash;

    private void Awake()
    {
        instance = this;
        _input = GetComponent<PlayerInputHandler>();
        _rb = GetComponent<Rigidbody2D>();
        dashEvent.AddListener(OnDash);
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DropBreadCrumbsCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDashing)
        {
            _rb.velocity = _movementSpeed * _input.movement.normalized;
            // _rb.AddForce(_movementSpeed * _input.movement.normalized * 600);
            // if (_rb.velocity.magnitude > _movementSpeed)
            // {
            //     _rb.velocity = _rb.velocity.normalized * _movementSpeed;
            // }
            if (_rb.velocity.magnitude > 0)
            {
                AudioManager.Instance.PlayFootstep();
            }
            else
            {
                AudioManager.Instance.StopFootstep();
            }
        }
        else
        {
            if (Time.time - _timeWhenDashed > _dashTime)
            {
                _isDashing = false;
                _rb.velocity = Vector2.zero;
            }
        }

        // If the player is at the center of the screen
        gunDirection = (_input.mousePosition - (Vector2)transform.position).normalized;
        _gun.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, gunDirection));

        _sp.flipX = gunDirection.x > 0;
        _spGun.flipY = gunDirection.x < 0;
        if (_wasFlipped != _sp.flipX)
        {
            _ropeAnchor.localPosition = new Vector3(-_ropeAnchor.localPosition.x, _ropeAnchor.localPosition.y, _ropeAnchor.localPosition.z);
        }
        _wasFlipped = _sp.flipX;

    }

    public void Damage(int amount)
    {
        if (_canBeDamaged && !_isDashing)
        {
            // Check for reduced damage power up
            CameraController.instance.CameraShake(0.3f, 2.5f);
            int n = Gun.instance.CheckForPowerUp(PowerUpType.defensive);
            currentLife -= (int)((float)amount / (n + 1));
            if (currentLife <= 0)
            {
                currentLife = 0;
                GameManager.instance.PlayerDied(transform.position);
                Destroy(gameObject);
                return;
            }
            else
            {
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.PlayerDamaged);
            }
            StartCoroutine(CanBeDamagedCoroutine());
            // TODO: Trigger invincibility frames
        }
    }

    public void OnDash()
    {
        if (_canDash)
        {
            Vector2 direction = _input.movement.normalized;
            if (direction != Vector2.zero && Gun.instance.TryToShootEnemyToDash(direction))
            {
                _isDashing = true;
                _rb.velocity = direction * _dashSpeed;
                _timeWhenDashed = Time.time;
                StartCoroutine(DashCooldownCoroutine());
            }
        }
    }

    private IEnumerator DashCooldownCoroutine()
    {
        _canDash = false;
        _psDash.Play();
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }

    private IEnumerator CanBeDamagedCoroutine()
    {
        _canBeDamaged = false;
        yield return new WaitForSeconds(_iFrameTime);
        _canBeDamaged = true;
    }
}
