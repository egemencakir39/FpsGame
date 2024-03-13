using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    [SerializeField] Transform camera1;

    [Header("Position")]

    [SerializeField] Vector3 MaxTargetPos;
    [SerializeField] Vector3 MinTargetPos;

    [SerializeField] Quaternion MaxTargetRot;
    [SerializeField] Quaternion MinTargetRot;

    Vector3 TargetPos;
    Vector3 OriginalPos;

    Quaternion TargetRot;
    Quaternion OriginalRot;

    [Header("")]

    Vector3 SlideVector;
    Quaternion SlideRot;

    [Header("Speed")]

    [SerializeField] float SlideSpeed;
    [SerializeField] float LerpSpeed;

    [SerializeField] bool Lerp;
    private void Start()
    {
        OriginalPos = camera1.localPosition;
        OriginalRot = camera1.localRotation;
    }
    private void Update()
    {
        /*
        if (Lerp)
        {
            SlideVector = Vector3.MoveTowards(SlideVector, TargetPos, SlideSpeed * Time.deltaTime);
            SlideRot = Quaternion.RotateTowards(SlideRot, TargetRot, SlideSpeed * Time.deltaTime * 7f);

            if (SlideVector == TargetPos)
            {
                Lerp = false;
            }
        }
        else
        {
            SlideVector = Vector3.MoveTowards(SlideVector, OriginalPos, SlideSpeed * Time.deltaTime);
            SlideRot = Quaternion.RotateTowards(SlideRot, OriginalRot, SlideSpeed * Time.deltaTime * 7f);
        }
        camera1.localPosition = Vector3.Lerp(camera1.position, SlideVector, LerpSpeed * Time.deltaTime);
        camera1.localRotation = Quaternion.Lerp(camera1.localRotation, SlideRot, LerpSpeed * Time.deltaTime * 7f);
        */
    }

    public void SetTarget()
    {
        TargetPos = OriginalPos + new Vector3(Random.Range(MinTargetPos.x, MaxTargetPos.x), Random.Range(MinTargetPos.y, MaxTargetPos.y), Random.Range(MinTargetPos.z, MaxTargetPos.z));
        TargetRot = OriginalRot * Quaternion.Euler(Random.Range(MinTargetRot.eulerAngles.x, MaxTargetRot.eulerAngles.x), Random.Range(MinTargetRot.eulerAngles.y, MaxTargetRot.eulerAngles.y), Random.Range(MinTargetRot.eulerAngles.z, MaxTargetRot.eulerAngles.z));
        Lerp = true;
    }
}