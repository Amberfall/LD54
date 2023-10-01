using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public static PowerUpsManager instance;
    [SerializeField] private PowerUp[] _powerUps;
    public int totalEnemyKilled;
    private int _currentEnemyKilled;
    [SerializeField] private int _enemyToKillForNextPowerUp = 10;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        totalEnemyKilled = 0;
        _currentEnemyKilled = 0;
    }

    public void AddKill()
    {
        totalEnemyKilled++;
        _currentEnemyKilled++;
    }

    public void RequestPowerUp(Vector3 position)
    {
        if (_currentEnemyKilled >= _enemyToKillForNextPowerUp)
        {
            _currentEnemyKilled = 0;
            Instantiate(_powerUps[Random.Range(0, _powerUps.Length)], position, Quaternion.identity);
        }
    }
}
