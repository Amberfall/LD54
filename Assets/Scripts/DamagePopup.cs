using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro _textMeshPro;
    [SerializeField] private TextMeshPro _textMeshProBG;
    [SerializeField] private float _amplitude = 1f;

    private void Awake()
    {
        _textMeshPro = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damageAmout)
    {
        _textMeshPro.text = damageAmout.ToString();
        _textMeshProBG.text = damageAmout.ToString();
        StartCoroutine(AnimationCoroutine());
    }

    private IEnumerator AnimationCoroutine()
    {
        float time = 0;
        float duration = 1;
        Vector3 origin = transform.position;
        while (time < duration)
        {
            transform.position = origin + _amplitude * Vector3.up * Mathf.Sin(time * Mathf.PI / duration);
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
