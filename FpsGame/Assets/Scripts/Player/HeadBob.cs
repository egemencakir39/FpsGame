using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] Transform Head;
    [SerializeField] Transform HeadParent;

    [Header("Variables")]

    [SerializeField] float BobFreq;
    [SerializeField] float HorizontalMagnitude;
    [SerializeField] float VerticalMagnitude;
    [SerializeField] float LerpSpeed;

    private float WalkingTime;
    private Vector3 TargetVector;

    
    private void Update()
    {
        SetHeadBob();
    }

    void SetHeadBob()
    {
        //if (CharacterMovement.Instance.TotalSpeed = 0f) WalkingTime = 0f;
       // else WalkingTime += Time.deltaTime;

        TargetVector = HeadParent.position + Setoffset(WalkingTime);
        Head.position = Vector3.Lerp(Head.position, TargetVector, LerpSpeed * Time.deltaTime);

        if((Head.position - TargetVector).magnitude <= 0.001f) Head.position = TargetVector;
    }

    Vector3 Setoffset(float Time)
    {
        float HorizontalOffset = 0f;
        float VerticalOffset = 0f;
        Vector3 Offset = Vector3.zero;
        if (Time > 0)
        {
            HorizontalOffset = Mathf.Cos(Time * BobFreq) * HorizontalMagnitude;
            VerticalOffset = Mathf.Sin(Time * BobFreq * 2f) * VerticalMagnitude;

            Offset = HeadParent.right * HorizontalOffset + HeadParent.up * VerticalOffset;
        }

        return Offset;
    }
}
