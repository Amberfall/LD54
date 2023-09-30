using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InanimateObject : Suckable
{
    protected override void HandleCollisionWhileShot(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(damage);
        }
        Destroy(gameObject);
    }

    protected override void HandleShoot()
    {
        gameObject.layer = LayerMask.NameToLayer("Shot");
    }

    protected override void GoToIdleState()
    {
        base.GoToIdleState();
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

}
