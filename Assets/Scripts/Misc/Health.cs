using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int> OnDamageTaken;
    public event Action playerDeing;

    [SerializeField] private int maxHealth = 100;
    [Header("ReadOnly")]
    [SerializeField] private int currentHealth;

    private bool dead = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (dead)
            return;

        currentHealth -= damage;
        OnDamageTaken?.Invoke(damage);

        if (currentHealth <= 0)
        {
            dead = true;
            playerDeing?.Invoke();
            Debug.Log(gameObject.name +" is dead");
        }
    }

    public void SetupSlider(out int maxHealth, out int currentHealth)
    {
        maxHealth = this.maxHealth;
        currentHealth = this.currentHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
