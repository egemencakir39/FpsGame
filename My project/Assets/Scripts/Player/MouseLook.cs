using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public static MouseLook Instance;
    [SerializeField] Transform Player;
    [SerializeField] Transform CameraParent;
    [SerializeField][Range(0f, 10f)] float Sensivity;
    float x;
    float y;
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
        x = Input.GetAxis("Mouse X") * Sensivity * Time.deltaTime * 15f;
        y += Input.GetAxis("Mouse Y") * Sensivity * Time.deltaTime * 15f;

        y = Mathf.Clamp(y, -80f, 80f);

        CameraParent.localRotation= Quaternion.Euler(-y, 0f, 0f);
        Player.Rotate(Vector3.up * x);
    }

}
