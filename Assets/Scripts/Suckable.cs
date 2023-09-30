using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suckable : MonoBehaviour
{
    public Sprite sprite;
    public int bagSpace;
    public int damage;
    public bool isSucked;
    public bool isShot;
    private Vector3 _playerPosition;
    public Vector2 velocity;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        if (!isShot)
        {
            // Turn this into a state machine
            if (isSucked)
            {
                GetComponent<Rigidbody2D>().velocity = 4 * (_playerPosition - transform.position).normalized;
                if ((_playerPosition - transform.position).magnitude < 1f)
                {
                    // TODO: Check if enough place in the bag
                    Gun.instance.suckables.Add(this);
                    gameObject.SetActive(false);
                }
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sucker")
        {
            isSucked = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Sucker")
        {
            _playerPosition = other.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Sucker")
        {
            isSucked = false;
        }
    }

    public void Shoot(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
        isShot = true;
    }
}
