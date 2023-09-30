using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InanimateObject : Suckable
{
    protected override void HandleCollisionWhileShot()
    {
        Destroy(gameObject);
    }
}
