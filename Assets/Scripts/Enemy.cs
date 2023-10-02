using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Enemy : Suckable, IDamageable
{
    public enum EnemyType
    {
        Bubble,
        Lanky,
        Toothy,
    }
    public EnemyType enemyType;
    private Player _player;
    public float movementSpeed;
    [SerializeField] private Lifebar _lifebar;
    private Toothy _toothy;
    protected override void Initialization()
    {
        base.Initialization();
        _player = Player.instance;
        _lifebar = GetComponentInChildren<Lifebar>();
        _toothy = GetComponent<Toothy>();
    }
    protected override void IdleState()
    {
        base.IdleState();

        if (_player != null)
        {
            Vector3 distance = _player.transform.position - transform.position;
            if (enemyType == EnemyType.Bubble)
            {
                rb.velocity = distance.normalized * movementSpeed;
            }
            else if (enemyType == EnemyType.Lanky)
            {
                if (distance.magnitude > GetComponent<Shooter>().minDistanceToShoot)
                {
                    rb.velocity = distance.normalized * movementSpeed;
                }
                else
                {
                    rb.velocity = distance.normalized * movementSpeed / 3f;
                    GetComponent<Shooter>().Shoot();
                }
            }
            else if (enemyType == EnemyType.Toothy)
            {
                if (!_toothy.isCharging)
                {
                    if (_toothy.canCharge)
                    {
                        if (distance.magnitude < _toothy.distanceToCharge)
                        {
                            _toothy.Charge();
                            rb.velocity = distance.normalized * _toothy.chargeSpeed;
                        }
                        else
                        {
                            rb.velocity = distance.normalized * movementSpeed;
                        }
                    }
                    else
                    {
                        rb.velocity = distance.normalized * movementSpeed;
                    }
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    protected override void HandleCollisionWhileShot(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(damage);
            if (other.transform.tag == "Enemy")
            {
                // Self damage
                Damage(damage / 2);
                VFXManager.instance.SpawnCrashEffect(transform.position);
            }
        }
    }

    protected override void HandleOnTriggerStay(Collider2D other)
    {
        if (other.tag == "Player" && suckableState == SuckableState.Idle)
        {
            other.GetComponent<IDamageable>().Damage(damage);
        }
    }

    protected override void HandleDeath()
    {
        base.HandleDeath();
        PowerUpsManager.instance.AddKill();
        PowerUpsManager.instance.RequestPowerUp(transform.position);
    }

    protected override void HandleDamageTaken(int amount)
    {
        DamagePopupManager.instance.CreatePopup(transform.position + Vector3.up * _yDamagePopupOffset, amount);
        _lifebar.SetLife((float)currentLife / maxLife);
    }

    protected override void HandleShoot()
    {
        gameObject.layer = LayerMask.NameToLayer("Shot");
    }

    protected override void GoToIdleState()
    {
        base.GoToIdleState();
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

}
