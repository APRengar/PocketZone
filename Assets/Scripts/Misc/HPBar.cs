using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Health health;
    private Slider healthsBar;

    public void SetupBar(int minHP, int maxHP, int currentHP)
    {
        healthsBar.minValue = minHP;
        healthsBar.maxValue = maxHP;
        healthsBar.value = currentHP;
    }

    private void OnEnable() 
    {
        if (health != null)
        {
            health.OnDamageTaken += UpdateHealthBar;
        }
    }
    private void OnDisable() 
    {
        if (health != null)
        {
            health.OnDamageTaken -= UpdateHealthBar;
        }
    }


    void UpdateHealthBar(int damageTaken)
    {
        if (health != null)
        {
            healthsBar.value = -damageTaken;
            if (healthsBar.value < 0)
            {
                healthsBar.value = 0;
            }
        }
    }
}
