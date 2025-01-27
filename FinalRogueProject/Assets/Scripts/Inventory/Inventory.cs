using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements.Experimental;
using System;


public class Inventory : MonoBehaviour
{

    public static event Action<List<InventoryItem>> OnInventoryChanged;

        
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();


    
    public void OnEnable()
    {
        FireBall.OnFireBall += Add;
    }
    public void Add(ItemData itemData)
    {
       
        InventoryItem newItem = new InventoryItem(itemData);
        inventory.Add(newItem);
        itemDictionary.Add(itemData, newItem);
        Debug.Log($"Added {itemData.displayName}");
        OnInventoryChanged?.Invoke(inventory);
    }

    public void Remove(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            inventory.Remove(item);
            itemDictionary.Remove(itemData);
            OnInventoryChanged?.Invoke(inventory);

        }
    }
    
}
