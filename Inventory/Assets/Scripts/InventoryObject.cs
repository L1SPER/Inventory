using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "ItemObject/InventoryObject")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> inventory = new List<InventorySlot>();
    public void AddItem(Item item)
    {
        if(!item.itemObject.isStackable)
        {
            inventory.Add(new InventorySlot(item, 1, item.id));
        }
        else if(item.itemObject.isStackable)
        {
            inventory.Add(new InventorySlot(item, item.amount,item.id));
        }
    }
}
[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int ID;
    public int amount;
    public InventorySlot(Item item,int amount, int ID)
    {
        this.item = item;
        this.amount = amount;
        this.ID = ID;
    }
    public void AddAmount(int amount)
    {
        this.amount += amount;
    }
}
