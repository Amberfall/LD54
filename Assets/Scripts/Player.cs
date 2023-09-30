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

    [Header("Dash stuff")]
    private bool _isDashing;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashCooldown;
    private float _timeWhenDashed;
    private bool _canDash = true;

    [Header("Life stuff")]
    public int maxLife;
    public int currentLife;

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

    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDashing)
        {
            _rb.velocity = _movementSpeed * _input.movement.normalized;
        }
        else
        {
            if (Time.time - _timeWhenDashed > _dashTime)
            {
                _isDashing = false;
                _rb.velocity = Vector2.zero;
            }
        }
        // _rb.AddForce(_movementSpeed * _input.movement.normalized * 600);
        // if (_rb.velocity.magnitude > _movementSpeed)
        // {
        //     _rb.velocity = _rb.velocity.normalized * _movementSpeed;
        // }

        // If the player is at the center of the screen
        gunDirection = (_input.mousePosition - (Vector2)transform.position).normalized;
        _gun.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, gunDirection));
    }

    public void Damage(int amount)
    {
        if (!_isDashing)
        {
            currentLife -= amount;
            if (currentLife <= 0)
            {
                // DIE
            }
            // TODO: Trigger invincibility frames
        }
    }

    public void OnDash()
    {
        if (_canDash)
        {
            _isDashing = true;
            _rb.velocity = _input.movement.normalized * _dashSpeed;
            _timeWhenDashed = Time.time;
            StartCoroutine(DashCooldownCoroutine());
        }
    }

    private IEnumerator DashCooldownCoroutine()
    {
        _canDash = false;
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }
}
