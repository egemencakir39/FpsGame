using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using JetBrains.Annotations;
using UnityEngine.UI;




public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public AnimationClip animationClip;
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

    [SerializeField] public bool Fire;
    [SerializeField] int CurrentAmmo;
    [SerializeField] private float fireSpeed = 100f;
    [SerializeField] private float Gravity = 9.81f;
  

    RaycastHit FireRaycast;
    [SerializeField] float FireRange;

    [Header("Reload Variables")]

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

    [Header("Bullet Holes & Particles")]

    [SerializeField] GameObject[] BulletHoles;

    [Header("Indýcators")]

    public Text CurrentAmmoText;
    public Text TotalAmmoText;

    [Header("Bullet Scatter")]

    [SerializeField] Quaternion MaxScatter;
    [SerializeField] Quaternion MinScatter;
    [SerializeField] Quaternion MaxScatterRun;

    Quaternion CurrentScatter;

    [Header("ReCoil")]

    [SerializeField] Vector2 MaxRecoil;
    [SerializeField] Vector2 MinRecoil;


    [Header("CameraShake")]
    [SerializeField] private CameraShake CameraShakeControl;
   // [SerializeField] private float ShakeIntensity = 5;
   // [SerializeField] private float ShakeTime = 1;

    private void Start()
    {
       // _cameraShake = FindObjectOfType<CameraShake>();
    }
    private void Update()
    {
        Inputs();
        SetTotalAnmmo();
        
    }
    
    void Inputs()
    {
        WeaponTransform.localRotation = MouseLook.Instance.CameraParent.localRotation;
        CurrentAmmoText.text = CurrentAmmo.ToString();
        TotalAmmoText.text = TotalAmmo.ToString();

        if (Input.GetMouseButton(0) && !Reload && CurrentAmmo > 0 && Availability && !isFiring)
        {
           startFire();
        }
        if ((Input.GetKeyDown(KeyCode.R) || CurrentAmmo <= 0) && TotalAmmo > 0 && CurrentAmmo != MaxAmmo && !isFiring)
        {
            startReload();
        }
    }
    public void startFire()
    {
        CameraShakeControl.ShakeCamera();
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
       Invoke("ResetIsFiring", .38f);
        CurrentAmmo--;

        if (Physics.Raycast(CameraController.Instance.Camera.position,SetScatter() * CameraController.Instance.Camera.forward, out FireRaycast, FireRange))
        {

            GameObject copyBulletHole = Instantiate(BulletHoles[UnityEngine.Random.Range(0, BulletHoles.Length)], FireRaycast.point, Quaternion.LookRotation(FireRaycast.normal));
            copyBulletHole.transform.parent = FireRaycast.transform;
            Destroy(copyBulletHole, 15f);
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

                
                

            }
        }
        CreateMuzzleFlash();
        SetRecoil();
       
       
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

    Quaternion SetScatter()
    {
        if (CharacterMovement.Instance.IsWalking)
        {
            CurrentScatter = Quaternion.Euler(UnityEngine.Random.Range(-MaxScatter.eulerAngles.x, MaxScatter.eulerAngles.x), UnityEngine.Random.Range(-MaxScatter.eulerAngles.y, MaxScatter.eulerAngles.y), UnityEngine.Random.Range(-MaxScatter.eulerAngles.z, MaxScatter.eulerAngles.z));
        }
        else if (CharacterMovement.Instance.IsRuning)
        {
            CurrentScatter = Quaternion.Euler(UnityEngine.Random.Range(-MaxScatterRun.eulerAngles.x, MaxScatterRun.eulerAngles.x), UnityEngine.Random.Range(-MaxScatterRun.eulerAngles.y, MaxScatterRun.eulerAngles.y), UnityEngine.Random.Range(-MaxScatterRun.eulerAngles.z, MaxScatterRun.eulerAngles.z));
        }
        else
        {
            CurrentScatter = MinScatter;
        }

        return CurrentScatter;
    }

    void SetRecoil()
    {
        float X = UnityEngine.Random.Range(MaxRecoil.x, MinRecoil.x);
        float Y = UnityEngine.Random.Range(MaxRecoil.y, MinRecoil.y);

        MouseLook.Instance.AddRecoil(X, Y);
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

    

    public void AddAmmo(WeaponManager.AmmoTypes Type, int Amount)
    {
        if (Type == AmmoTypes._12ga)
        {
            _12ga += Amount;
        }
        else if (Type == AmmoTypes._5_56)
        {
            _5_56 += Amount;
        }
        else if (Type == AmmoTypes._9mm)
        {
            _9mm += Amount;
        }
        else if (Type == AmmoTypes._45cal)
        {
            _45cal += Amount;
        }
        else if (Type == AmmoTypes._7_62)
        {
            _7_62 += Amount;
        }
    }

}
