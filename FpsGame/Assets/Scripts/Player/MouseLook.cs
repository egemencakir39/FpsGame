using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public static MouseLook Instance;
    [SerializeField] Transform Player;
    [SerializeField] public Transform CameraParent;
    [SerializeField][Range(0f, 10f)] float Sensivity;
    float X;
    float Y;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Cursor.visible = false; //mouse kapatma
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        MouseControl();
    }
    void MouseControl()//Mouse Kontrolcüsü
    {
        X += Input.GetAxis("Mouse X") * Sensivity * Time.deltaTime * 15f;
        Y += Input.GetAxis("Mouse Y") * Sensivity * Time.deltaTime * 15f;

        Y = Mathf.Clamp(Y, -80f, 80f);

        CameraParent.localRotation= Quaternion.Euler(-Y, 0f, 0f);
        Player.localRotation = Quaternion.Euler(0f, X, 0f);
    }
    public void AddRecoil(float x, float y)
    {
        X += x;
        Y += y;
    }
}
