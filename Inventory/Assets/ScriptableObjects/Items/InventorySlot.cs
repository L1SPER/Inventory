using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public Items[] allowedItems = new Items[0];
    public Item item = new Item();
    [System.NonSerialized]
    public UserInterface parent;
    public int parentId;
    public int id;
    public int amount;
    public int slotId;
   
    public ItemObject GetItemObject()
    {
        return item.id >= 0 ? parent.inventory.database.GetItem[item.id] : null;
    }
    public InventorySlot() => UpdateSlot(new Item(), -1, 0,-1);
    public InventorySlot(Item _item, int _id, int _amount, int _slotId) => UpdateSlot(_item, _id, _amount, _slotId);
    public InventorySlot(Item _item, int _id, int _amount, int _parentId,int _slotId) => UpdateSlot(_item, _id, _amount,_parentId, _slotId);
    public void RemoveItem() => UpdateSlot(new Item(), -1, 0,-1);

    public void UpdateSlot(InventorySlot _slot)
    {
        this.item = _slot.item;
        this.amount=_slot.amount;
        this.id= _slot.id;
        this.slotId = _slot.slotId;
    }
    public void UpdateSlot(Item _item, int _id, int _amount,int _slotId)
    {
        this.item = _item;
        this.amount = _amount;
        this.id = _id;
        this.slotId= _slotId;
    }
    public void UpdateSlot(Item _item, int _id, int _amount,int _parentId,int _slotId)
    {
        this.item = _item;
        this.amount = _amount;
        this.id = _id;
        this.parentId = _parentId;
        this.slotId=_slotId;
    }
    public void UpdateSlot(Item _item, int _id, int _amount, int _parentId, int _slotId, Items[] _allowedItems)
    {
        this.item = _item;
        this.amount = _amount;
        this.id = _id;
        this.parentId = _parentId;
        this.slotId = _slotId;
        UpdateAllowedItems(_allowedItems);
    }
    public void UpdateAllowedItems(Items[] _allowedItems)
    {
        for (int i = 0; i < _allowedItems.Length; i++)
        {
            this.allowedItems[i] = _allowedItems[i];
        }
    }
    public void AddAmount(int amount)
    {
        this.amount += amount;
    }
}