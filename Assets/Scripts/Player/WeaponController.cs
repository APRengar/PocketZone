using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] CircleCollider2D weaponRange;
    [SerializeField] Transform aimer;

    [SerializeField] Transform weaponAttachmentPoint;
    [SerializeField] WeaponBase currentWeapon;
    [SerializeField] List<WeaponBase> weapons;
    private int equipedWeaponID = 0;

    [Header("ReadOnly")]
    [SerializeField] EnemyController selectedTarget;
    [SerializeField] List<EnemyController> enemies;
    [SerializeField] int selectedEnemyID;

    private float upperArmAngle = 35;

    private void Start() 
    {
        if (currentWeapon != null & !weapons.Contains(currentWeapon))
            weapons.Add(currentWeapon);
    }

    private void FixedUpdate() 
    {
        Aim();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "DamageReciever")
        {
            enemies.Add(other.GetComponentInParent<EnemyController>());
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.GetComponentInParent<EnemyController>() != null)
        {
            other.GetComponentInParent<EnemyController>().DeactivateSelector();
            enemies.Remove(other.GetComponentInParent<EnemyController>());
            selectedTarget = null;
        }
    }

    private void Aim()
    {
        

        // If there is no enemies in the list - target=null, weapon aiming=default
        if (enemies.Count < 1)
        {
            selectedTarget = null;
            aimer.localRotation = Quaternion.Euler(0, 0, 0);
            return;
        }

        if (!selectedTarget)
        {
            selectedTarget = enemies[0];
            selectedEnemyID = 0;
            selectedTarget.ActivateSelector();
        }
        else
        {
            if (selectedTarget.GetComponent<Health>().GetCurrentHealth() <= 0)
            {
                enemies.Remove(selectedTarget);
                ChangeTarget();
                return;
            }
            Vector3 localEnemyPosition = aimer.parent.InverseTransformPoint(selectedTarget.GetAim().position);
            Vector3 localDirection = (localEnemyPosition - aimer.localPosition).normalized;
            float angle = Mathf.Atan2(localDirection.y, localDirection.x) * Mathf.Rad2Deg;
            aimer.localRotation = Quaternion.Euler(0, 0, angle + upperArmAngle);
        }
    }

    public void ChangeTarget()
    {
        selectedTarget.DeactivateSelector();
        int i = selectedEnemyID + 1;
        if (i < enemies.Count)
        {
            selectedTarget.DeactivateSelector();
            selectedTarget = enemies[selectedEnemyID + 1];
            selectedEnemyID = selectedEnemyID + 1;
            selectedTarget.ActivateSelector();
        }
        else
        {
            if(enemies.Count <= 0)
            {
                Debug.Log("No enemies");
                return;
            }
            selectedTarget = enemies[0];
            selectedEnemyID = 0;
            selectedTarget.ActivateSelector();
        }
    }

    public void EquipWeapon(IWeapon weapon)
    {
        currentWeapon = (WeaponBase)weapon;
    }
    
    public void EquipNextWeapon()
    {
        int i = equipedWeaponID + 1;
        if (i < weapons.Count)
        {
            currentWeapon = weapons[equipedWeaponID + 1];
            equipedWeaponID = equipedWeaponID + 1;
        }
        else
        {
            currentWeapon = weapons[0];
            equipedWeaponID = 0;
        }
    }

    public void ShootAtTheTarget()
    {
        
        if (selectedTarget == null)
        {
            return;
        }

        if (currentWeapon != null)
        {
            currentWeapon.Attack();
        }
    }
}
