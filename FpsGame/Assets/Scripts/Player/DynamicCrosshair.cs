using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicCrosshair : MonoBehaviour
{
    [SerializeField] RectTransform Crosshair;
   

    [Header("crosshair v1")]

    [SerializeField] float maxSize;
    [SerializeField] float minSize;
    [SerializeField] float currentSize;
    [SerializeField] float maxSizeV2;

    [Header("")]

    [SerializeField] float Speed;

    private void Update()
    {
        Inputs();
        SetSize();
    }

    void SetSize()
    {
        Crosshair.sizeDelta = new Vector2(currentSize, currentSize); 
    }

    void Inputs()
    {
        if (!CharacterMovement.Instance.IsWalking && !CharacterMovement.Instance.IsRuning)
        {
            SetMin();
        }

        else if (CharacterMovement.Instance.IsWalking)
        {
            SetMax();
        }
        if(CharacterMovement.Instance.IsRuning)
        {
            SetMaxV2();
        } 
    }
    void SetMaxV2()
    {
        currentSize = Mathf.Lerp(currentSize, maxSizeV2, Speed * Time.deltaTime);
    }
    void SetMin()
    {
        currentSize = Mathf.Lerp(currentSize, minSize, Speed * Time.deltaTime);
    }

    void SetMax()
    {
        currentSize = Mathf.Lerp(currentSize, maxSize, Speed * Time.deltaTime);
    }
}
