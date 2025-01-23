using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [System.Serializable]
    public class InventoryDataList
    {
        public List<InventoryData> inventoryItems;
    }
    [System.Serializable]
    public class InventoryData
    {
        public int itemID; // Unique identifier for the item
        public int quantity; // Quantity of the item
    }

    [System.Serializable]
    public class PlayerData
    {
        public float[] position; // Array to store x, y, z position
    }
    
    private static string saveFilePath => Application.persistentDataPath + "/inventory.json";
    private static string saveFilePathPos => Application.persistentDataPath + "/position.json";

    public static void SaveInventory(Dictionary<Item, int> items)
    {
        // Convert the inventory data to a serializable format
        List<InventoryData> inventoryDataList = new List<InventoryData>();
        foreach (var kvp in items)
        {
            inventoryDataList.Add(new InventoryData
            {
                itemID = kvp.Key.ID, // Use an ID or unique identifier for the item
                quantity = kvp.Value
            });
        }

        // Serialize the data into JSON
        string json = JsonUtility.ToJson(new InventoryDataList { inventoryItems = inventoryDataList }, true);

        // Save the JSON to a file
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Inventory saved to: " + saveFilePath);
    }

    public static Dictionary<Item, int> LoadInventory(List<Item> availableItems)
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("No save file found.");
            return new Dictionary<Item, int>();
        }

        // Read the JSON from the file
        string json = File.ReadAllText(saveFilePath);

        // Deserialize the JSON into a list of inventory data
        InventoryDataList inventoryDataList = JsonUtility.FromJson<InventoryDataList>(json);

        // Reconstruct the inventory dictionary from the deserialized data
        Dictionary<Item, int> loadedItems = new Dictionary<Item, int>();
        foreach (var data in inventoryDataList.inventoryItems)
        {
            Item item = availableItems.Find(i => i.ID == data.itemID);
            if (item != null)
            {
                loadedItems[item] = data.quantity;
            }
        }

        Debug.Log("Inventory loaded from: " + saveFilePath);
        return loadedItems;
    }


    public static void SavePlayerPosition(Vector3 position)
    {
        // Create a PlayerData object
        PlayerData data = new PlayerData
        {
            position = new float[] { position.x, position.y, position.z }
        };

        // Convert the object to JSON
        string json = JsonUtility.ToJson(data, true);

        // Write the JSON to a file
        File.WriteAllText(saveFilePathPos, json);

        Debug.Log("Player position saved to: " + saveFilePathPos);
    }
    public static Vector3 LoadPlayerPosition()
    {
        if (!File.Exists(saveFilePathPos))
        {
            Debug.LogWarning("No save file found. Returning default position.");
             SavePlayerPosition(Vector3.zero);
            return Vector3.zero; // Default position if no save file exists
           
        }

        // Read the JSON from the file
        string json = File.ReadAllText(saveFilePathPos);
        Debug.Log(json);

        // Convert the JSON back into a PlayerData object
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);

        // Return the position as a Vector3
        return new Vector3(data.position[0], data.position[1], data.position[2]);
    }
}
