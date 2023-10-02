using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<IDamageable>().Damage(_damage);
            Destroy(gameObject);
        }
    }
}
