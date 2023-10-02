using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private CrashEffect _crashEffect;
    [SerializeField] private BamEffect _bamEffect;

    public static VFXManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnCrashEffect(Vector3 position)
    {
        Instantiate(_crashEffect, position, Quaternion.identity);
        Instantiate(_bamEffect, position, Quaternion.identity);
    }
}
