using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxed : MonoBehaviour, IInteractable
{
    public string ItemName;
    public WeaponManager.AmmoTypes Type;
    public int Amount;

    string IInteractable.Name { get => ItemName; set => ItemName = value; }

    void IInteractable.Interact()
    {
        WeaponManager.Instance.AddAmmo(Type, Amount);
        Destroy(this.gameObject);
    }
}
