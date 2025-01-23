using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Health playerHP;
    [SerializeField] float secondToAnimateDeath = 1.5f ;
    [SerializeField] GameObject youDied;

    private void Start() 
    {
        playerHP = GetComponent<Health>();  
    }

    private void OnEnable() 
    {
        playerHP.playerDeing += ResestPlayer;
    }
    private void OnDisable() 
    {
        playerHP.playerDeing -= ResestPlayer;
    }
    
    private void ResestPlayer()
    {
        StartCoroutine(Death());
        
    }
    private IEnumerator Death()
    {
        yield return new WaitForSeconds (secondToAnimateDeath);
        Player.Instance.GetComponent<PlayerItemSlots>().UnequipAllItems();
        youDied.SetActive(true);
        yield return new WaitForSeconds (secondToAnimateDeath);
        youDied.SetActive(false);
        SaveSystem.SavePlayerPosition(Player.Instance.defaultSpawnPosition.position);
        Inventory.Instance.ClearInventory();
        LevelManager.Instance.ResetLevel();
    }

}
