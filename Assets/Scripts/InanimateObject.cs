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
}
