using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int> OnDamageTaken;
    public event Action playerDeing;
    
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnDamageTaken?.Invoke(damage);

        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead");
            playerDeing?.Invoke();
        }
    }
    public void SetMaxHealth(int healths)
    {
        maxHealth = healths;
        
    }
}
