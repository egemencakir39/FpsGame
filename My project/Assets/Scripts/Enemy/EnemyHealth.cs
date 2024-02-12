using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public static EnemyHealth Instance;
    [SerializeField] private int Maxhealth = 100;
    [SerializeField] private int CurrentHealth;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CurrentHealth = Maxhealth;
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }  
    }
    void Die()
    {
        Destroy(gameObject);
    }
}

