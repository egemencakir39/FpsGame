using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private int health = 100;
    

    private void OnEnable()
    {
        WeaponManager.takeDamage += WeaponManager_Takedamages;
    }

    private void WeaponManager_Takedamages()
    {

       // health -= WeaponManager.damage;
        if (health <= 0)
        {
            Die();
        }
    }



    void Die()
    {
        // D��man �l�m�yle ilgili i�lemler buraya yaz�labilir
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        WeaponManager.takeDamage -= WeaponManager_Takedamages;
    }
}

