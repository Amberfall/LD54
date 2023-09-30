using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    public static DamagePopupManager instance;
    [SerializeField] private Transform _damagePopupPrefab;
    private void Awake()
    {
        instance = this;
    }
    public void CreatePopup(Vector3 position, int amount)
    {
        Transform dp = Instantiate(_damagePopupPrefab, position, Quaternion.identity);
        dp.GetComponent<DamagePopup>().Setup(amount);
    }
}
