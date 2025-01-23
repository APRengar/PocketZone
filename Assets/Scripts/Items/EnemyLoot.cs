using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [SerializeField] List<Item> lootPool;
    [SerializeField] private Item itemToDrop;

    private void Start() 
    {
        RandomizeLoot();
    }

    private void RandomizeLoot()
    {
        int random = Random.Range(0, lootPool.Count);
        itemToDrop = lootPool[random];
    }

    public void DropLoot() 
    {
        itemToDrop.DropItem(transform.position);
    }
}
