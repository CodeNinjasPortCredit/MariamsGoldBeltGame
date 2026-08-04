using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]

public class InventoryItem
{
    public string itemName;
    public int quantity;
}

public class InventoryManager : MonoBehaviour
{
 
    public HashSet<InventoryItem> inventory = new HashSet<InventoryItem>();

    public void AddItem(string itemName)
    {
        InventoryItem existingItem = inventory.FirstOrDefault(item => item.itemName == itemName);

        if (existingItem != null)
        {
            existingItem.quantity++;
            Debug.Log("You have collected another" + itemName);
        }
        else
        {
            InventoryItem newItem = new InventoryItem { itemName = itemName, quantity = 1 };
            inventory.Add(newItem);
            Debug.Log("You have collected " + itemName);
        }
    }

    public bool HasItem (string itemName)
    {
        InventoryItem existingItem = inventory.FirstOrDefault(item => item.itemName == itemName);
        return existingItem != null && existingItem.quantity > 0;
    }
}
