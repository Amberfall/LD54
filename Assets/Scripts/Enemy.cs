using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Suckable
{
    private Player _player;
    public float movementSpeed;
    protected override void Initialization()
    {
        base.Initialization();
        _player = Player.instance;
    }
    protected override void IdleState()
    {
        base.IdleState();

        if (_player != null)
        {
            rb.AddForce((_player.transform.position - transform.position).normalized * movementSpeed);
            if (rb.velocity.magnitude > movementSpeed)
            {
                rb.velocity = rb.velocity.normalized * movementSpeed;
            }
            //rb.velocity = (_player.transform.position - transform.position).normalized * movementSpeed;
        }
    }

    protected override void HandleCollisionWhileShot(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(damage);
            if (other.transform.tag == "Enemy")
            {
                Damage(1);
            }
        }
    }

    protected override void HandleDamageTaken(int amount)
    {
        DamagePopupManager.instance.CreatePopup(transform.position + Vector3.up * _yDamagePopupOffset, amount);
    }

}
