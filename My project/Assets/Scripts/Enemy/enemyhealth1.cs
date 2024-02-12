using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhealth1 : MonoBehaviour
{
    public int maxHealth = 10000;
    [SerializeField] private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Düþmaný yok etme iþlemi burada gerçekleþtirilir.
        Destroy(gameObject);
    }
}