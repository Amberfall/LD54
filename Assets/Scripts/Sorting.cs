using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    const float globalYOffset = 24f;
    const float ppu = 16f;
    [SerializeField] private SpriteRenderer _sp;
    [SerializeField] private Transform _bottomTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _sp.sortingOrder = (int)(ppu * (globalYOffset - _bottomTransform.position.y));
    }
}
