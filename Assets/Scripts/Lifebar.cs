using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifebar : MonoBehaviour
{
    [SerializeField] private Transform _currentLifeTransform;

    public void SetLife(float ratio)
    {
        _currentLifeTransform.localScale = new Vector3(ratio, 1, 1);
        Debug.Log(ratio);
    }
}
