using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]
public class InventoryItem
{

    public ItemData itemData;
    public int stackSize;


 
    

    public InventoryItem(ItemData item)
    {
        itemData = item;    
    }


}
