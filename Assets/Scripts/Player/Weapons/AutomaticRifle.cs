using UnityEngine;

public class  AutomaticRifle : WeaponBase
{
    public override void Attack()
    {
        base.Attack();
        Debug.Log("Очередь из автомата!");
    }
}