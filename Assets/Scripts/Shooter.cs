using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _timeToShoot;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _spawnPointTransform;
    public float minDistanceToShoot;
    private bool _canShoot = true;

    private void OnEnable()
    {
        _canShoot = true;
    }

    public void Shoot()
    {
        if (Player.instance != null && _canShoot)
        {
            StartCoroutine(CanShootCoroutine());
            var b = Instantiate(_bulletPrefab, _spawnPointTransform.position, Quaternion.identity);
            b.SetVelocity((Player.instance.transform.position - _spawnPointTransform.position).normalized * _bulletSpeed);
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.EnemyShoot, _spawnPointTransform.position);
        }
    }

    private IEnumerator CanShootCoroutine()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_timeToShoot);
        _canShoot = true;
    }
}
