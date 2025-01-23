using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private int quantity;

    public void AssignItem(Item droppedItem)
    {
        item = droppedItem;
    }

    public Item PickupItem()
    {
        return item;
    }
    public int GetQuantity()
    {
        return quantity;
    }
}
