using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    damage_multiplier,
    defensive,
}

public class PowerUp : Suckable
{
    public PowerUpType powerUpType;
    public override void InBagEffect()
    {

    }
}
