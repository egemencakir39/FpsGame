using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   public static CameraController Instance;
    private void Awake()
    {
        Instance = this;
    }
    public Transform Camera;
    public Transform CameraParent;
}
