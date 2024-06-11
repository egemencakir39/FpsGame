using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Range(0.001f, 0.01f)]
    public float Amount = 0.002f;

    [Range(1f, 30f)]
    public float Frequency = 10f;

    [Range(10f, 100f)]
    public float Smooth = 10f;

    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.localPosition;
    }

    private void Update()
    {
        CheckForHeadbobTrigger();
    }

    private void CheckForHeadbobTrigger()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;
        if (inputMagnitude > 0)
        {
            StartHeadBob();
        }
        else
        {
            ResetPosition();
        }
    }

    private void StartHeadBob()
    {
        Vector3 pos = _startPos;
        pos.y += Mathf.Sin(Time.time * Frequency) * Amount;
        pos.x += Mathf.Cos(Time.time * Frequency / 2f) * Amount * 1.4f;
        transform.localPosition = Vector3.Lerp(transform.localPosition, pos, Smooth * Time.deltaTime);
    }

    private void ResetPosition()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _startPos, Smooth * Time.deltaTime);
    }
}
