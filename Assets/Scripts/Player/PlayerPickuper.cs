using UnityEngine;

public class PlayerPickuper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Pickups pickup = other.GetComponent<Pickups>();
        if (pickup == null)
            return;
        Item itemToAdd = pickup.PickupItem();
        int Quantity = pickup.GetQuantity();
        if (itemToAdd == null)
            return;
        Inventory inventory = Inventory.Instance;
        if (inventory == null)
            return;
        if (inventory.AddItemToInventory(itemToAdd, Quantity))
            Destroy(other.gameObject); // Only destroy the pickup if the item was successfully added
        else
            Debug.LogWarning("Could not add item to inventory. Inventory might be full.");
        
    }
}
