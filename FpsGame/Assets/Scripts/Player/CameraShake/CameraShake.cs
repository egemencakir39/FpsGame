using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCam;
    private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;
    [SerializeField] private float intensity = 5f;
    [SerializeField] private float shakeTime = 2f;
    private void Awake()
    {
        _virtualCam = GetComponent<CinemachineVirtualCamera>();
        _multiChannelPerlin = _virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ResetIntensity();

    }
    public void ShakeCamera()
    {
        _multiChannelPerlin.m_AmplitudeGain = intensity;
        StartCoroutine(WaitTime(shakeTime));

    }

    IEnumerator WaitTime(float shakeTime)
    {
        yield return new WaitForSeconds(shakeTime);
        ResetIntensity();

    }
    void ResetIntensity()
    {
        _multiChannelPerlin.m_AmplitudeGain = 0f;
    }
}
