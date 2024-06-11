using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilShake : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource _screenShake;
    [SerializeField] float _powerAmount;

    public void ScreenShake(Vector3 dir)
    {
        _screenShake.GenerateImpulseWithVelocity(dir);
    }
}
