using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Suckable, IDamageable
{
    private Player _player;
    public float movementSpeed;
    [SerializeField] private Lifebar _lifebar;
    protected override void Initialization()
    {
        base.Initialization();
        _player = Player.instance;
        _lifebar = GetComponentInChildren<Lifebar>();
    }
    protected override void IdleState()
    {
        base.IdleState();

        if (_player != null)
        {
            Vector3 target = _player.transform.position;
            rb.velocity = (_player.transform.position - transform.position).normalized * movementSpeed;
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
