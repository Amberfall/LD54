using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler instance;
    public InputMaster inputMaster;
    public Vector2 movement;
    public Vector2 mousePosition;
    private Camera _mainCam;

    private void Awake()
    {
        instance = this;
        _mainCam = Camera.main;
        inputMaster = new InputMaster();
        inputMaster.Player.Enable();
        inputMaster.Player.Shoot.performed += OnShootPerformed;
        inputMaster.Player.Suck.performed += OnSuckPerformed;
        inputMaster.Player.Suck.canceled += OnSuckCanceled;
        inputMaster.Player.Dash.performed += OnDashPerformed;
    }

    public void Update()
    {
        // Movement
        movement = inputMaster.Player.Movement.ReadValue<Vector2>();
        mousePosition = _mainCam.ScreenToWorldPoint(inputMaster.Player.MousePosition.ReadValue<Vector2>());
    }

    public void OnShootPerformed(InputAction.CallbackContext context)
    {
        Gun.shootEvent.Invoke();
    }
    public void OnSuckPerformed(InputAction.CallbackContext context)
    {
        Gun.trigerSuckingEvent.Invoke(true);
    }
    public void OnSuckCanceled(InputAction.CallbackContext context)
    {
        Gun.trigerSuckingEvent.Invoke(false);
    }

    public void OnDashPerformed(InputAction.CallbackContext context)
    {
        Player.dashEvent.Invoke();
    }

}

