using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    Animator Animation;

    private void Start()
    {
        Animation = GetComponent<Animator>();
    }
    public void Setbool(string AnimationID, bool AnimationBoolean)
    {
        Animation.SetBool(AnimationID, AnimationBoolean);
    }
    public void SetTrigger(string AnýmatýonID)
    {
        Animation.SetTrigger(AnýmatýonID);
    }
    public void endFire()
    {
        WeaponManager.Instance.endFire();
    }

    public void endReload()
    {
        WeaponManager.Instance.endReload();
    }
    public void WeaponDown()
    {
        WeaponManager.Instance.ChangeWeapon();
    }

    public void SetAvailibity(int Index)
    {
        WeaponManager.Instance.Availability = Index == 0 ? false : true;
    }
   
}
