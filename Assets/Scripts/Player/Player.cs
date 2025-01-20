using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private IWeapon currentWeapon;

    private PlayerController playerController;
    private PlayerPartsRandomiser skinRandomiser;

    private void Start() 
    {
        playerController = GetComponent<PlayerController>();
        skinRandomiser = GetComponent<PlayerPartsRandomiser>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) // ЛКМ или аналогичная кнопка
        {
            currentWeapon?.Attack();
        }
    }

    public void EquipWeapon(IWeapon weapon)
    {
        currentWeapon = weapon;
    }
    
    public void EquipNextWeapon()
    {

    }
}
