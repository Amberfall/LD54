using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private Color _backgroundColor;
    [SerializeField] private Transform _playerDying;
    public void SetAlpha(float a)
    {
        _background.color = new Color(_backgroundColor.r, _backgroundColor.g, _backgroundColor.b, a);
    }
    public void SetScale(float s)
    {
        _playerDying.localScale = s * Vector3.one;
    }
}
