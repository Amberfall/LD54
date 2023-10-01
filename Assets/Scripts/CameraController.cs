using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private float _intensity;
    private IEnumerator _shakeCoroutine;


    private void Awake()
    {
        instance = this;
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void CameraShake(float duration, float intensity)
    {
        if (intensity >= _intensity)
        {
            if (_shakeCoroutine != null)
            {
                StopCoroutine(_shakeCoroutine);
            }
            _shakeCoroutine = CameraShakeCoroutine(duration, intensity);
            StartCoroutine(_shakeCoroutine);
        }
    }

    private IEnumerator CameraShakeCoroutine(float duration, float intensity)
    {
        _intensity = intensity;
        _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        yield return new WaitForSeconds(duration);
        _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        _intensity = 0;
    }
}
