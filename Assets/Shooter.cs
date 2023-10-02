using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private Transform _playerTransform;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _timeToShoot;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _spawnPointTransform;
    void Start()
    {
        _playerTransform = Player.instance.transform;
        StartCoroutine(ShootingCoroutine());
    }

    private IEnumerator ShootingCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToShoot);
            if (_playerTransform != null)
            {
                var b = Instantiate(_bulletPrefab, _spawnPointTransform.position, Quaternion.identity);
                b.SetVelocity((_playerTransform.position - _spawnPointTransform.position).normalized * _bulletSpeed);
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.EnemyShoot, _spawnPointTransform.position);
            }
        }
    }
}
