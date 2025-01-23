using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Basic Info")]
    public int ID;
    [SerializeField] private string itemName;         
    [SerializeField] private string description;     
    [SerializeField] private Sprite icon;

    [Header("Settings")]
    [SerializeField] private int quantity;
    [SerializeField] private GameObject pickupPrefab; 
    [SerializeField] private GameObject inventoryItem;

    // Public properties to access private fields
    public string ItemName => itemName;
    public string Description => description;
    public Sprite Icon => icon;
    public int Quantity => quantity;

    /// <summary>
    /// Drops the item into the world at the specified position.
    /// </summary>
    public void DropItem(Vector3 position)
    {
        if (pickupPrefab != null)
        {
            GameObject droppedItem = Instantiate(pickupPrefab, position, Quaternion.identity);
            droppedItem.GetComponent<Pickups>().AssignItem(this);
            Debug.Log($"Dropped {itemName} at {position}");
        }
        else
        {
            Debug.LogWarning("Pickup prefab is not assigned for this item!");
        }
    }

    public GameObject GetInventoryPrefab()
    {
        return inventoryItem;
    }
}
