using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    [SerializeField] protected int damage = 10; // Урон оружия
    [SerializeField] protected int attackRate = 1; // Скорость атаки 
    [SerializeField] Transform bulletSpawnPoint; //точка спавна пуль

    protected bool canAttack = true;

    public virtual void Attack()
    {
        if (!canAttack) return;
        Debug.Log($"{name} атакует, урон: {damage}");
        StartCoroutine(AttackCooldown());
    }

    protected IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1f / attackRate);
        canAttack = true;
    }
}
