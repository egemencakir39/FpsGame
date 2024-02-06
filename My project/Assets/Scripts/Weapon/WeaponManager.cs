using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] Transform WeaponTransform;

    [SerializeField] bool Fire;
    [SerializeField] bool Reload;
    [SerializeField] bool isFiring = false;

    [Header("Animastions")]

    [SerializeField] AnimationController Animation;
    [SerializeField] string FireI_ID;
    [SerializeField] string FireII_ID;
    [SerializeField] string Reload_ID;
    [SerializeField] string WeaponDown_ID;

    
    private void Update()
    {
        Inputs();
    }
    void Inputs()
    {
        WeaponTransform.localRotation = MouseLook.Instance.CameraParent.localRotation;

        if (Input.GetMouseButton(0))
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
    }
    private void ResetIsFiring()
    {
        isFiring = false;
    }
    public void endFire()
    {
        Fire = false;
        Animation.Setbool (FireI_ID, Fire);
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
    }
    public void ChangeWeapon()
    {

    }
}
