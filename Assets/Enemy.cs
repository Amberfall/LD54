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

}
