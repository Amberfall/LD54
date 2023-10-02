using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Suckable
{
    public int life = 20;
    [SerializeField] private Sprite[] _sprites;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = _sprites[Random.Range(0, _sprites.Length)];
    }

    protected override void GoToIdleState()
    {
        base.GoToIdleState();
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
