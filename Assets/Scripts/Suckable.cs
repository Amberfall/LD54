using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suckable : MonoBehaviour
{
    public Sprite sprite;
    public int bagSpace;
    public int damage;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>().sprite;
    }
}
