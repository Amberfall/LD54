using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private Transform _playerTransform;
    private Vector2 _mousePosition;
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private float _maxDistanceFromPlayer = 3;

    private void Start()
    {
        _playerTransform = Player.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance != null)
        {
            Vector3 averagePosition = (Vector3)PlayerInputHandler.instance.mousePosition - _playerTransform.position;
            if (averagePosition.magnitude > _maxDistanceFromPlayer)
            {
                averagePosition = averagePosition.normalized * _maxDistanceFromPlayer;
            }
            transform.position = _playerTransform.position + averagePosition + _cameraOffset;
        }
    }
}
