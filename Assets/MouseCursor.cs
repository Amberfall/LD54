using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private InputMaster _inputMaster;
    private Camera _mainCam;
    void Awake()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Player.Enable();
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var mp = _mainCam.ScreenToWorldPoint(_inputMaster.Player.MousePosition.ReadValue<Vector2>());
        transform.position = new Vector3(mp.x, mp.y, 0);
    }
}
