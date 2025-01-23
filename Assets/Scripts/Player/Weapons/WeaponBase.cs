using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    [SerializeField] protected int damage = 10; // Урон оружия
    [SerializeField] protected int attackRate = 1; // Скорость атаки 
    
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] protected Transform bulletSpawnPoint; //точка спавна пуль
    [SerializeField] ParticleSystem flash;
    [SerializeField] ParticleSystem smoke;

    protected bool canAttack = true;

    public virtual void Attack()
    {
        if (!canAttack) return;
        if (Inventory.Instance.CheckAmmoQuantity() > 0)
            StartCoroutine(AttackCooldown());
        else
        {
            Debug.Log("Not Enought Ammo!");
        }
    }

    protected IEnumerator AttackCooldown()
    {
        canAttack = false;
        flash.Play();
        smoke.Play();
        Inventory.Instance.ConsumeBullet();
        GameObject spawnedBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        spawnedBullet.GetComponent<Bullet>().SetDamage(damage);
        yield return new WaitForSeconds(1f / attackRate);
        canAttack = true;
    }
}
