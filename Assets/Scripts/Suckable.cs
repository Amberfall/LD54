using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Suckable : MonoBehaviour, IDamageable
{
    public enum SuckableState
    {
        Idle,
        Sucked,
        Shot,
        Rejected,
    }
    public enum SuckableType
    {
        furniture,
        enemy,
        powerup,
        consumable,
    }
    protected Rigidbody2D rb;
    public SuckableState suckableState = SuckableState.Idle;
    protected float _time;
    [SerializeField] protected float _idleFriction;
    [SerializeField] protected float _rejectionFriction;
    [SerializeField] protected float _shotFriction;
    [SerializeField] protected float _rejectionTime;
    [SerializeField] protected float _maxShotTime;
    [SerializeField] protected float _suckedVelocity;
    [SerializeField] protected float _rejectionVelocity;

    [Header("Suckable Data")]
    public Sprite sprite;
    public int size;
    public int baseDamage;
    public int damage;
    public bool isPowerUp;
    public SuckableType suckableType;

    [Header("Life stuff")]
    public int maxLife;
    public int currentLife;
    [SerializeField] protected float _yDamagePopupOffset;
    public bool isDash;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        currentLife = maxLife;
    }

    private void Start()
    {
        Initialization();
    }

    private void Update()
    {
        switch (suckableState)
        {
            case SuckableState.Idle:
                IdleState();
                break;
            case SuckableState.Sucked:
                SuckedState();
                break;
            case SuckableState.Shot:
                ShotState();
                break;
            case SuckableState.Rejected:
                RejectedState();
                break;
        }
    }

    protected virtual void Initialization()
    {
        GoToIdleState();
    }

    protected virtual void IdleState()
    {

    }
    protected virtual void SuckedState()
    {
        if (Player.instance != null)
        {
            Vector2 distance = Player.instance.transform.position - transform.position;
            rb.velocity = _suckedVelocity * (1 + 2 / distance.magnitude) * distance.normalized;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    protected virtual void ShotState()
    {
        if (Time.time - _time > _maxShotTime || rb.velocity.magnitude < 0.5f)
        {
            GoToIdleState();
        }
    }
    protected virtual void RejectedState()
    {
        if (Time.time - _time > _rejectionTime)
        {
            GoToIdleState();
        }
    }

    protected virtual void HandleCollisionWhileShot(Collision2D other)
    {
        GoToIdleState();
    }

    protected virtual void HandleCollisionWhileRejected(Collision2D other)
    {
        GoToIdleState();
    }

    protected virtual void HandleOnTriggerStay(Collider2D other)
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (suckableState == SuckableState.Idle)
        {
            if (other.tag == "Sucker")
            {
                suckableState = SuckableState.Sucked;
            }
        }
        else if (suckableState == SuckableState.Sucked)
        {
            if (other.tag == "SuckableRange")
            {
                if (!other.GetComponentInParent<Gun>().SuckedRequest(this))
                {
                    GoToRejectedState();
                }
            }
        }

        HandleOnTriggerStay(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Sucker")
        {
            GoToIdleState();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (suckableState == SuckableState.Shot)
        {
            HandleCollisionWhileShot(other);
        }
        else if (suckableState == SuckableState.Rejected)
        {
            HandleCollisionWhileRejected(other);
        }
    }

    public void Shoot(Vector2 velocity)
    {
        suckableState = SuckableState.Shot;
        _time = Time.time;
        rb.drag = _shotFriction;
        rb.velocity = velocity;
        HandleShoot();
    }

    protected virtual void GoToIdleState()
    {
        suckableState = SuckableState.Idle;
        _time = Time.time;
        rb.drag = _idleFriction;
        rb.velocity = Vector2.zero;
    }

    protected virtual void GoToRejectedState()
    {
        suckableState = SuckableState.Rejected;
        rb.velocity = -rb.velocity.normalized * _rejectionVelocity;
        rb.drag = _rejectionFriction;
        _time = Time.time;
    }

    public virtual void InBagEffect()
    {

    }

    public void Damage(int amount)
    {
        if (!isPowerUp)
        {
            currentLife -= amount;
            if (currentLife <= 0)
            {
                HandleDeath();
                Destroy(gameObject);
            }
            HandleDamageTaken(amount);
        }
    }

    protected virtual void HandleDamageTaken(int amount)
    {

    }

    protected virtual void HandleDeath()
    {

    }

    protected virtual void HandleShoot()
    {

    }
}
