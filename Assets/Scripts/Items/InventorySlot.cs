using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public Item slotItemHolder;

    bool equiped = false;

    public void DeleteItem()
    {
        if (Inventory.Instance.CheckItemQuantity(slotItemHolder) == 1)
        {
            Unequip();
        }
        Inventory.Instance.RemoveItemFromInventory(slotItemHolder, 1);
    }   

    public void EquipItem()
    {
        if (!slotItemHolder) return;
        if (!slotItemHolder.GetInventoryPrefab().GetComponent<InventoryItem>()) return;
        
        if (equiped) Unequip();
        else  Equip();
    }

    private void Equip()
    {
        InventoryItem slotItem = slotItemHolder.GetInventoryPrefab().GetComponent<InventoryItem>();
        if (!slotItem)
        {
            Debug.Log("This is not eqipable");
            return;
        }
        int bodyPartToEqip = slotItem.BodyPart;
        int pieceToEqip = slotItem.ArmorPiece;
        equiped = true;
        Player.Instance.GetComponent<PlayerItemSlots>().EquipItem(bodyPartToEqip, pieceToEqip);
    }

    private void Unequip()
    {
        InventoryItem slotItem = slotItemHolder.GetInventoryPrefab().GetComponent<InventoryItem>();
        if (!slotItem)
        {
            Debug.Log("This is not eqipable");
            return;
        }
        int bodyPartToEqip = slotItem.BodyPart;
        equiped = false;
        Player.Instance.GetComponent<PlayerItemSlots>().UnequipItem(bodyPartToEqip);
    }
}
