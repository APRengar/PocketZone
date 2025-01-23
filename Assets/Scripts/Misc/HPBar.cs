using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] TextMeshProUGUI hpText;
    private Slider healthsBar;

    private void Awake() 
    {
        healthsBar = GetComponentInChildren<Slider>();
    }

    private void Start() 
    {
        SetupBar();
    }

    public void SetupBar()
    {
        int maxHP;
        int currHP;
        health.SetupSlider(out maxHP, out currHP);
        healthsBar.maxValue = maxHP;
        healthsBar.value = currHP;
        hpText.text = currHP.ToString();
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
            healthsBar.value -= damageTaken;
            hpText.text = healthsBar.value.ToString();
            if (healthsBar.value < 0)
            {
                healthsBar.value = 0;
            }
        }
    }
}
