using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private PlayerInputHandler _input;
    private Rigidbody2D _rb;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Transform _gun;
    public Vector2 gunDirection;

    private void Awake()
    {
        instance = this;
        _input = GetComponent<PlayerInputHandler>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = _movementSpeed * _input.movement.normalized;
        // If the player is at the center of the screen
        gunDirection = (_input.mousePosition - (Vector2)transform.position).normalized;
        _gun.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, gunDirection));
    }
}
