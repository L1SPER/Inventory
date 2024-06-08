using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditorInternal;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour,ISerializationCallbackReceiver
{
    public InventoryObject[] inventoryObjects;
    public Dictionary<int, InventoryObject> GetInventoryObject=new Dictionary<int, InventoryObject>();
    private static InventoryManager inventoryManager;
    public static InventoryManager Instance{ get=> inventoryManager; set=> inventoryManager=value; }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < inventoryObjects.Length; i++)
        {
            inventoryObjects[i].id = i;
            GetInventoryObject.Add(i, inventoryObjects[i]);
        }
    }
    public void OnBeforeSerialize()
    {
        GetInventoryObject = new Dictionary<int, InventoryObject>();
    }
    public void SwapSlots(InventorySlot _slot1, InventorySlot _slot2)
    {
        InventorySlot slot1 = GetInventoryObject[_slot1.parentId].Container.FindInventorySlot(_slot1.slotId);
        InventorySlot slot2 = GetInventoryObject[_slot2.parentId].Container.FindInventorySlot(_slot2.slotId);
        if(CanPlaceInSlot(slot1, slot2))
        {
            InventorySlot tempSlot = new InventorySlot(_slot2.item, _slot2.id, _slot2.amount, _slot2.parentId, _slot2.slotId);
            GetInventoryObject[_slot2.parentId].Container.FindInventorySlot(_slot2.slotId).UpdateSlot(_slot1.item, _slot1.id, _slot1.amount, tempSlot.parentId, tempSlot.slotId);
            GetInventoryObject[_slot1.parentId].Container.FindInventorySlot(_slot1.slotId).UpdateSlot(tempSlot.item, tempSlot.id, tempSlot.amount, _slot1.parentId, _slot1.slotId);
        }
    }
    public void SortInventory(int id)
    {
        GetInventoryObject[id].Sort();
    }
    public bool CanPlaceInSlot(InventorySlot _slot1, InventorySlot _slot2)
    {
        //inventory to inventory 0->0
        if (_slot1.allowedItems.Length <= 0 && _slot2.allowedItems.Length <= 0)
        {
            Debug.Log(0);
            return true;
        }
        //inventory to equipment 0->1
        else if(_slot1.allowedItems.Length<=0&&_slot2.allowedItems.Length>0)
        {
            for (int i = 0; i < _slot2.allowedItems.Length; i++)
            {
                if (_slot1.item.itemObject.itemType == _slot2.allowedItems[i])
                {
                    Debug.Log(1);
                    return true;
                }
            }
        }
        //equipment to inventory 1->0 
        else if (_slot1.allowedItems.Length > 0&& _slot2.allowedItems.Length<=0)
        {
            if (_slot2.item.itemObject == null) return true;
            for (int i = 0; i < _slot1.allowedItems.Length; i++)
            {
                if (_slot2.item.itemObject.itemType == _slot1.allowedItems[i])
                {
                    Debug.Log(1);
                    return true;
                }
            }
        }
        //equipment to equipment 1->1 
        else if (_slot1.allowedItems.Length>0 && _slot2.allowedItems.Length > 0)
        {
            if(_slot1.allowedItems.Length>=_slot2.allowedItems.Length)
            {
                for (int i = 0; i < _slot1.allowedItems.Length; i++)
                {
                    for (int j = 0; j < _slot2.allowedItems.Length; j++)
                    {
                        if (_slot1.allowedItems[i] == _slot2.allowedItems[j])
                            if (IsItemInAllowdItems(_slot1, _slot2))
                                return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < _slot2.allowedItems.Length; i++)
                {
                    for (int j = 0; j < _slot1.allowedItems.Length; j++)
                    {
                        if (_slot1.allowedItems[j] == _slot2.allowedItems[i]) 
                            if (IsItemInAllowdItems(_slot1, _slot2))
                                return true;
                    }
                }
            }
        }
        return false;
    }
    public bool IsItemInAllowdItems(InventorySlot _slot1, InventorySlot _slot2)
    {
        int counter = 0;
        if(_slot2.item.itemObject== null) return true;
        for (int i = 0; i < _slot1.allowedItems.Length; i++)
        {
            if (_slot2.item.itemObject.itemType == _slot1.allowedItems[i]) counter++;
        }
        for (int i = 0; i < _slot2.allowedItems.Length; i++)
        {
            if (_slot1.item.itemObject.itemType == _slot2.allowedItems[i]) counter++;
        }
        if (counter == 2) return true;
        else return false;
    }
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            DontDestroyOnLoad(Instance);
        }
    }
}
