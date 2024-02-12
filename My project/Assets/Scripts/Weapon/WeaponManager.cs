using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    private void Awake()
    {
        Instance = this;
    }
   
    [SerializeField] Transform WeaponTransform;

    public bool Availability;
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
    [SerializeField] private float fireSpeed = 100f;
    [SerializeField] private float Gravity = 9.81f;
  

    RaycastHit FireRaycast;
    [SerializeField] float FireRange;

    [Header("Reload Variaables")]

    [SerializeField] bool Reload;
    [SerializeField] int TotalAmmo;
    [SerializeField] int MaxAmmo;
    [SerializeField] AmmoTypes Type;
 
    public enum AmmoTypes
    {
        _5_56,
        _7_62,
        _9mm,
        _45cal,
        _12ga

    }


    [Header("Ammo Types")]


    [SerializeField] int _5_56;
    [SerializeField] int _7_62;
    [SerializeField] int _9mm;
    [SerializeField] int _45cal;
    [SerializeField] int _12ga;

    [Header("Muzzle Flash")]

    [SerializeField] Transform WeaponTip;
    [SerializeField] GameObject MuzzleFlash;
    [SerializeField] ParticleSystem BulletShells;

    [Header("Damage")]

    
    [SerializeField] int _5_56Damage = 20;
    [SerializeField] int _7_62Damage = 30;
    [SerializeField] int _9mmDamage = 15;
    [SerializeField] int _45calDamage = 25;
    [SerializeField] int _12gaDamage = 35;
    
   

    private void Update()
    {
        Inputs();
        SetTotalAnmmo();

    }
    void Inputs()
    {
        WeaponTransform.localRotation = MouseLook.Instance.CameraParent.localRotation;

        if (Input.GetMouseButton(0) && !Reload && CurrentAmmo > 0 && Availability)
        {
            if (!isFiring)
            {
                startFire();
            }

        }
        if ((Input.GetKeyDown(KeyCode.R) || CurrentAmmo <= 0) && TotalAmmo > 0 && CurrentAmmo != MaxAmmo && !Fire)
        {
            startReload();
        }
    }
    public void startFire()
    {
        isFiring = true;
        Fire = true;
        if (CurrentAmmo <= 1)
        {
            Animation.Setbool(FireII_ID, Fire);
        }
        else
        {
            Animation.Setbool(FireI_ID, Fire);
        }
        Invoke("ResetIsFiring", .4f);
        CurrentAmmo--;

        if (Physics.Raycast(CameraController.Instance.Camera.position, CameraController.Instance.Camera.forward, out FireRaycast, FireRange))
        {
            if (FireRaycast.transform.CompareTag("Enemy"))
            {
              int damage = 0;
                switch (Type)
                {
                    case AmmoTypes._5_56:
                        damage = _5_56Damage;
                        break;
                    case AmmoTypes._7_62:
                        damage = _7_62Damage;
                        break;
                    case AmmoTypes._9mm:
                        damage = _9mmDamage;
                        break;
                    case AmmoTypes._45cal:
                        damage = _45calDamage;
                        break;
                    case AmmoTypes._12ga:
                        damage = _12gaDamage;
                        if (FireRaycast.transform.GetComponent<Rigidbody>() != null)
                            FireRaycast.transform.GetComponent<Rigidbody>().AddForce(-FireRaycast.normal * 150f);
                        break;
                }
                EnemyHealth enemyHealth = FireRaycast.transform.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
                Debug.Log("Düþman vuruldu! Hasar: " + damage);
                
            }
        }
         CreateMuzzleFlash();
    }
    private void ResetIsFiring()
    {
        isFiring = false;
    }
    public void endFire()
    {
        Fire = false;
        Animation.Setbool(FireI_ID, Fire);
        Animation.Setbool(FireII_ID, Fire);
    }

    void CreateMuzzleFlash()
    {
        GameObject MuzzleFlashCopy = Instantiate(MuzzleFlash, WeaponTip.position, WeaponTip.rotation, WeaponTip);
        Destroy(MuzzleFlashCopy, 5f);

        BulletShells.Play();
    }

    public void startReload()
    {
        Reload = true;
        Animation.Setbool(Reload_ID, Reload);
    }
    public void endReload()
    {
        Reload = false;
        Animation.Setbool(Reload_ID, Reload);

        int Amount = SetReloadAmount(TotalAmmo);
        CurrentAmmo += Amount;
        if (Type == AmmoTypes._7_62)
            _7_62 -= Amount;

        else if (Type == AmmoTypes._5_56)
            _5_56 -= Amount;

        else if (Type == AmmoTypes._9mm)
            _9mm -= Amount;

        else if (Type == AmmoTypes._45cal)
            _45cal -= Amount;

        else if (Type == AmmoTypes._12ga)
            _12ga -= Amount;
    }

    void SetTotalAnmmo()
    {
     if(Type == AmmoTypes._7_62)
            TotalAmmo = _7_62;

     else if (Type == AmmoTypes._5_56)
            TotalAmmo = _5_56;

     else if (Type == AmmoTypes._9mm)
            TotalAmmo = _9mm;

     else if (Type == AmmoTypes._45cal)
            TotalAmmo = _45cal;

     else if (Type == AmmoTypes._12ga)
            TotalAmmo = _12ga;
    }

    int SetReloadAmount(int InventoryAmount)
    {
        int AmountNeeded = MaxAmmo - CurrentAmmo;

        if (AmountNeeded < InventoryAmount)
        {
            return AmountNeeded;
        }
        else
        {
            return InventoryAmount;
        }
        

        
    }

    public void ChangeWeapon()
    {

    }

  
}
