using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    damage_multiplier,
    defensive,
    movement,
}

public class PowerUp : Suckable
{
    public PowerUpType powerUpType;
    private float _lifeTime = 7f;
    [SerializeField] private SpriteRenderer _sp;


    public override void InBagEffect()
    {

    }

    protected override void IdleState()
    {
        base.IdleState();

        if (Time.time - _time > _lifeTime)
        {
            Destroy(gameObject);
        }
        else if (Time.time - _time > _lifeTime - 3)
        {
            float t = 2 * Mathf.PI * (Time.time - _time - _lifeTime - 3);
            _sp.color = new Color(1, 1, 1, Mathf.Abs(Mathf.Cos(t)));
        }
        else
        {
            _sp.color = new Color(1, 1, 1, 1);
        }
    }

    protected override void SuckedState()
    {
        base.SuckedState();
        _sp.color = new Color(1, 1, 1, 1);
    }

    private void OnEnable()
    {
        _sp.color = new Color(1, 1, 1, 1);
    }
}
