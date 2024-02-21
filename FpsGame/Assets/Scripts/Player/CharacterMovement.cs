using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    
    public static CharacterMovement Instance;
    [SerializeField] CharacterController CC;
    float Horizontal;
    float Vertical;
   

    [Header("Transform")]
    [SerializeField] Transform CharacterBody;
    [SerializeField] Transform GroundCheck;

    [Header("Movement")]

    public bool IsWalking;
    public bool IsRuning;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;
    

    [Header("Gravity")]

    Vector3 GravityVector;
    [SerializeField] float GravityAc = -9.81f;
    [SerializeField] bool Isgrounded;
    [SerializeField] LayerMask GroundLayer;
    private void Awake()
    {
        Instance = this;
    }
    private void FixedUpdate()
    {
        Movement();
        Gravity();
    }
    private void Update()
    {
        jump();
        CheckMovement();
    }
    void Movement()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        Vector3 Move = CharacterBody.right * Horizontal + CharacterBody.forward * Vertical;
        CC.Move(Move * TotalSpeed() * Time.deltaTime);
    }
    void CheckMovement()
    {
        if ((Horizontal != 0f || Vertical != 0f) && (Horizontal != 0f || Vertical != 0f))
        {
            if (TotalSpeed() == runSpeed)
            {
                IsRuning = true;
                IsWalking = false;
            }
            if (TotalSpeed() == walkSpeed)
            {
                IsRuning = false;
                IsWalking = true;
            }
        }
        else
        {
            IsRuning = false;
            IsWalking = false;
        }
    }

    void Gravity()
    {
        Isgrounded = Physics.CheckSphere(GroundCheck.position, 0.4f, GroundLayer);

        if (!Isgrounded)
        
            GravityVector.y += GravityAc * Mathf.Pow(Time.deltaTime, 2);
        
        else if (GravityVector.y < 0f)
        
            GravityVector.y = -0.15f;
        
        GravityVector.y += GravityAc * Mathf.Pow(Time.deltaTime, 2);
        CC.Move(GravityVector);
    }
    public float TotalSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            return runSpeed;
        else
            return walkSpeed;
        
    }
    void jump()
    {
        if (Isgrounded && Input.GetButtonDown("Jump"))
        GravityVector.y = Mathf.Sqrt(jumpForce * -2f * GravityAc / 1000f);

    }
}


