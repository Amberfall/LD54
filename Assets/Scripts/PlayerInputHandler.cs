using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerInputHandler : MonoBehaviour
{
    public InputMaster inputMaster;
    public Vector2 movement;
    public Vector2 mousePosition;
    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main;
        inputMaster = new InputMaster();
        inputMaster.Player.Enable();
        inputMaster.Player.Shoot.performed += OnShootPerformed;
        inputMaster.Player.Suck.performed += OnSuckPerformed;
        inputMaster.Player.Suck.performed += OnSuckPerformed;
    }

    public void Update()
    {
        // Movement
        movement = inputMaster.Player.Movement.ReadValue<Vector2>();
        mousePosition = _mainCam.ScreenToWorldPoint(inputMaster.Player.MousePosition.ReadValue<Vector2>());
    }

    public void OnShootPerformed(InputAction.CallbackContext context)
    {
    }
    public void OnSuckPerformed(InputAction.CallbackContext context)
    {
    }

}

