using UnityEngine;

public class Pistol : WeaponBase
{
    public override void Attack()
    {
        base.Attack();
        Debug.Log("Выстрел из пистолета!");
    }
}