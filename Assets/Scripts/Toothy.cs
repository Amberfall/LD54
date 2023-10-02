using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toothy : MonoBehaviour
{
    public bool canCharge;
    [SerializeField] private float _timeToCharge;
    public float distanceToCharge;
    public float chargeSpeed;
    private Rigidbody2D _rb;
    public bool isCharging;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        canCharge = true;
        isCharging = false;
    }

    private IEnumerator CanChargeCoroutine()
    {
        canCharge = false;
        yield return new WaitForSeconds(_timeToCharge);
        canCharge = true;
    }

    private IEnumerator IsChargingCoroutine()
    {
        isCharging = true;
        yield return new WaitForSeconds(1f);
        isCharging = false;
    }

    public void Charge()
    {
        StartCoroutine(CanChargeCoroutine());
        StartCoroutine(IsChargingCoroutine());
    }
}
