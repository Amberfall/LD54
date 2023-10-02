using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    public static DamagePopupManager instance;
    [SerializeField] private Transform _damagePopupPrefab;
    [SerializeField] private Transform _healthPopupPrefab;
    private void Awake()
    {
        instance = this;
    }
    public void CreatePopup(Vector3 position, int amount)
    {
        Transform dp = Instantiate(_damagePopupPrefab, position, Quaternion.identity);
        dp.GetComponent<DamagePopup>().Setup(amount);
    }

    public void CreateHealthPopup(Vector3 position, int amount)
    {
        Transform dp = Instantiate(_healthPopupPrefab, position, Quaternion.identity);
        dp.GetComponent<DamagePopup>().Setup(amount);
    }
}
