using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLife : MonoBehaviour
{
    private Player _player;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    void Start()
    {
        _player = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance != null)
        {
            _textMeshProUGUI.text = _player.currentLife.ToString() + "/" + _player.maxLife.ToString();
        }
    }
}
