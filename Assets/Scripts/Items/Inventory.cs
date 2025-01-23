using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] float inventorySwitchDuration = 0.5f;

    [Header("ReadOnly")]
    [SerializeField] bool isVisible = false;
    [SerializeField] int initialAmmo = 40;
    [SerializeField] Item ammo;
    [SerializeField] TextMeshProUGUI currentAmmo;
    
    private CanvasGroup canvasGroup;

    [SerializeField] private int maxSlots = 15;
    [SerializeField] private GameObject slotPrefab; // Prefab for an inventory slot.
    [SerializeField] private Transform slotsParent;
    [SerializeField] Sprite emptySlot;
    private List<GameObject> slotInstances = new List<GameObject>(); // To track slot instances.
    [SerializeField] private List<Item> availableItems;

    private Dictionary<Item, int> items = new Dictionary<Item, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // Initialize empty slots.
        for (int i = 0; i < maxSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotsParent);
            ValidateSlotPrefab(slot);// Ensure the slot is correctly set up.
            slotInstances.Add(slot);
            ClearSlotUI(slot);
        }
    }

    private void ValidateSlotPrefab(GameObject slot)
    {
        if (slot.transform.childCount < 3)
        {
            Debug.LogError("Slot prefab must have at least 3 children: Background, Icon, and Quantity!");
        }
    }

    private void Update() 
    {
        currentAmmo.text = CheckAmmoQuantity().ToString();
    }

    private void Start() 
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        items = SaveSystem.LoadInventory(availableItems);
        if (items.Count == 0) 
        {
            items.Add(ammo, initialAmmo);
        }
        UpdateInventoryUI();

        AutoEquipItems();
    }

    public void ConsumeBullet()
    {
        if (items.ContainsKey(ammo) && items[ammo] > 0)
        {
            items[ammo] -= 1;
            UpdateInventoryUI();
        }
        else
        {
            Debug.LogWarning("No ammo to consume!");
        }
    }

#region Items Mamangement

    public bool AddItemToInventory(Item item, int quantity)
    {
        if (items.ContainsKey(item))
        {
            // Update the quantity of the existing item
            items[item] += quantity;
            UpdateInventoryUI();
            return true;
        }

        // Check if there’s space for a new item
        if (items.Count >= maxSlots)
        {
            Debug.LogWarning("Inventory is full! Cannot add new item.");
            return false;
        }

        // Add the new item with its quantity
        items[item] = quantity;
        UpdateInventoryUI();
        // Debug.Log($"Added new item: {item.name} with quantity: {quantity}. Total items: {items.Count}/{maxSlots}");
        return true;
    }

    public bool RemoveItemFromInventory(Item item, int quantity)
    {
        if (!items.ContainsKey(item)) //if there is no item in inventory - debug error;
        {
            // Debug.LogWarning($"Item {item.name} not found in inventory.");
            return false;
        }

        // if (items[item] < quantity) //if we try to windraw 3 bullets, but we have only 2 - debug error
        // {
        //     // Debug.LogWarning($"Insufficient quantity of {item.name} to remove. Current quantity: {items[item]}");
        //     return false;
        // }

        items[item] -= quantity;
        if (items[item] <= 0)
        {
            items.Remove(item);

            // Debug.Log($"Item {item.name} removed from inventory.");
        }
        
        else
        {
            Debug.Log($"Removed {quantity} of {item.name}. Remaining quantity: {items[item]}");
        }
        UpdateInventoryUI();
        return true;
    }

    public int CheckItemQuantity(Item item)
    {
        return items.TryGetValue(item, out int quantity) ? quantity : 0;
    }

    public int CheckAmmoQuantity()
    {
        return items.TryGetValue(ammo, out int quantity) ? quantity : 0;
    }
#endregion

#region UI
    public void ToggleVisibility()
    {
        // if (canvasGroup.alpha == (isVisible ? 1f : 0f)) return; // Prevent redundant toggles.
        StopAllCoroutines(); // Stops any ongoing coroutine to prevent conflicts
        StartCoroutine(OnVisibilityChanged());
    }
    
    private IEnumerator OnVisibilityChanged()
    {
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;
        float targetAlpha = isVisible ? 0f : 1f;

        while (elapsedTime < inventorySwitchDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / inventorySwitchDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        canvasGroup.interactable = !isVisible;
        canvasGroup.blocksRaycasts = !isVisible;

        isVisible = !isVisible;
    }
    private void UpdateInventoryUI()
    {
        // Clear all slots.
        foreach (var slot in slotInstances)
        {
            ClearSlotUI(slot);
        }

        // Update slots with items from the dictionary.
        int index = 0;
        foreach (var kvp in items)
        {
            if (index >= slotInstances.Count) break;

            var slot = slotInstances[index];
            var item = kvp.Key;
            var quantity = kvp.Value;

            UpdateSlotUI(slot, item, quantity);
            index++;
        }
    }


    private void UpdateSlotUI(GameObject slot, Item item, int quantity)
    {
        GameObject background = slot.transform.GetChild(0).gameObject;
        Image icon = slot.transform.GetChild(1).GetComponent<Image>(); // Assuming child 0 is the icon.
        TextMeshProUGUI quantityText = slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>(); // Child 1 is the quantity text.

        icon.sprite = item.Icon; // Assign the item's sprite.
        slot.GetComponent<InventorySlot>().slotItemHolder = item;
        if (quantity == 1)
        {
            quantityText.text = "";
            background.SetActive(false);
        }
        else
        {
            quantityText.text = quantity.ToString();
            background.SetActive(true);
        }
        if (item != ammo)
        {
            slot.transform.GetChild(3).gameObject.SetActive(true);
            slot.transform.GetChild(4).gameObject.SetActive(true);
        }
    }

    private void ClearSlotUI(GameObject slot)
    {
        Image icon = slot.transform.GetChild(1).GetComponent<Image>();
        TextMeshProUGUI quantityText = slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        icon.sprite = emptySlot; // Clear the icon.
        slot.GetComponent<InventorySlot>().slotItemHolder = null;
        quantityText.text = ""; // Clear the quantity text.

        slot.transform.GetChild(0).gameObject.SetActive(false);
        slot.transform.GetChild(3).gameObject.SetActive(false);
        slot.transform.GetChild(4).gameObject.SetActive(false);
    }

    public void ClearInventory()
    {
        items.Clear();
        SaveSystem.SaveInventory(items);
    }


#endregion

    public void SaveGame()
    {
        SaveSystem.SaveInventory(items);
    }
    private void OnApplicationQuit()
    {
        // Save the inventory on game exit
        SaveSystem.SaveInventory(items);
    }

    private void AutoEquipItems()
    {
        foreach(Transform inventoryItem in slotsParent)
        {
            inventoryItem.GetComponent<InventorySlot>().EquipItem();
        }
    }
}
