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
            VFXManager.instance.SpawnCrashEffect(transform.position);
        }
        else
        {
            VFXManager.instance.SpawnCrashEffect(transform.position);
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

    public void PlayAnimation()
    {
        GetComponentInChildren<Animator>().Play("Furniture");
    }

}
