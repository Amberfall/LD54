using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BamEffect : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;

    void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = _sprites[Random.Range(0, _sprites.Length)];
        Destroy(gameObject, 0.5f);
    }


}
