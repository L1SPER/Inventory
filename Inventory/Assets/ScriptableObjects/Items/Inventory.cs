using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Inventory:ISerializationCallbackReceiver
{
    public InventorySlot[] Slots = new InventorySlot[30];
    public Dictionary<int, InventorySlot> InventorySlots=new Dictionary<int, InventorySlot>();
    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].RemoveItem();
        }
    }
    public int FindInventorySlotId(InventorySlot _slot)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].slotId== _slot.slotId)
            {
                return Slots[i].slotId;
            }
        }
        return -1;
    }
    public InventorySlot FindInventorySlot(int _slotId)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].slotId == _slotId)
            {
                return Slots[i];
            }
        }
        return null;
    }

    public void OnBeforeSerialize()
    {
       
    }
    public void OnAfterDeserialize()
    {
        InventorySlots=new Dictionary<int, InventorySlot>();
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].slotId = i;
        }
    }
}