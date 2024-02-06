using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] Transform WeaponTransform;

    public bool Availability;
    [SerializeField] bool Reload;
    [SerializeField] bool isFiring = false;

    [Header("Animastions")]

    [SerializeField] AnimationController Animation;
    [SerializeField] string FireI_ID;
    [SerializeField] string FireII_ID;
    [SerializeField] string Reload_ID;
    [SerializeField] string WeaponDown_ID;

    [Header("Fire Variables")]

    [SerializeField] bool Fire;
    [SerializeField] int CurrentAmmo;
    [SerializeField] int MaxAmmo;


    RaycastHit FireRaycast;
    [SerializeField] float FireRange;


    private void Update()
    {
        Inputs();


    }
    void Inputs()
    {
        WeaponTransform.localRotation = MouseLook.Instance.CameraParent.localRotation;

        if (Input.GetMouseButton(0) && !Reload && CurrentAmmo > 0)
        {
            if (!isFiring)
            {
                startFire();
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            startReload();
        }
    }
    public void startFire()
    {
        isFiring = true;
        Fire = true;
        Animation.Setbool(FireI_ID, Fire);
        Invoke("ResetIsFiring", .5f);
        CurrentAmmo--;

        if (Physics.Raycast(CameraController.Instance.Camera.position, CameraController.Instance.Camera.forward, out FireRaycast, FireRange))
        {
            if (FireRaycast.transform.GetComponent<Rigidbody>() != null)
                FireRaycast.transform.GetComponent<Rigidbody>().AddForce(-FireRaycast.normal * 1000f);


        }
    }
    private void ResetIsFiring()
    {
        isFiring = false;
    }
    public void endFire()
    {
        Fire = false;
        Animation.Setbool(FireI_ID, Fire);
    }
    public void startReload()
    {
        Reload = true;
        Animation.Setbool(Reload_ID, Reload);
        CurrentAmmo = MaxAmmo;
    }
    public void endReload()
    {
        Reload = false;
        Animation.Setbool(Reload_ID, Reload);
    }
    public void ChangeWeapon()
    {

    }

  
}
