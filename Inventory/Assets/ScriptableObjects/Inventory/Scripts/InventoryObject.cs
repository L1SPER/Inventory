using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "ItemObject/InventoryObject")]
public class InventoryObject : ScriptableObject
{
    public int id;
    public string savePath;
    public string Name;
    public Inventory Container;
    public ItemDatabaseObject database;
    public UserInterface type;
    public InventorySlot[] GetSlots => Container.Slots;
    public void AddItem(Item item)
    {
        if(!IsInventoryFull())
        {
            if (!database.GetItem[item.id].isStackable)
            {
                GetSlots[FindLastEmptySlotNumber()].UpdateSlot(item, item.id, item.amount, FindLastEmptySlotNumber());
            }
            else if (database.GetItem[item.id].isStackable)
            {
                for (int i = 0; i < Container.Slots.Length; i++)
                {
                    //Envanterde item zaten var mý diye itemi arýyorum
                    if (GetSlots[i].id==item.id)
                    {
                        //Envanterde olan iteme ekledim.
                        if(database.GetItem[item.id].slotAmountMax >= item.amount + GetSlots[i].amount)
                        {
                            GetSlots[i].amount += item.amount;
                        }
                        //Envanterde olmayan itemi ekledim.
                        else
                        {
                            int remain = item.amount + GetSlots[i].amount - database.GetItem[item.id].slotAmountMax;
                            GetSlots[i].amount = database.GetItem[item.id].slotAmountMax;

                            //Fazlasýný envanterde yer varsa slot oluþturdum yoksa itemin miktarýný azalttým
                            if(!IsInventoryFull())
                            {
                                //Arada bir yerde eðer boþ slot varsa diye son boþ slota koyuyorum
                                GetSlots[FindLastEmptySlotNumber() ].UpdateSlot(item, item.id, remain, FindLastEmptySlotNumber());
                            }
                            else
                            {
                                item.amount = remain;
                            }
                        }
                    }
                    //Envanterde yoksa itemi ekliyorum.
                    else
                    {
                        GetSlots[FindLastEmptySlotNumber()].UpdateSlot(item, item.id, item.amount, FindLastEmptySlotNumber());
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("You cant add more items !!!");
        }
    }
    [ContextMenu("Save")]
    public void Save()
    {
        string jsonString = JsonUtility.ToJson(Container);
        File.WriteAllText(string.Concat(Application.persistentDataPath, savePath), jsonString);
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            string readJson = File.ReadAllText(string.Concat(Application.persistentDataPath, savePath));
            Inventory newContainer = JsonUtility.FromJson<Inventory>(readJson);
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if(newContainer.Slots[i].item == null)
                    break;
                GetSlots[i].UpdateSlot(newContainer.Slots[i].item,
                                                newContainer.Slots[i].item.id,
                                                newContainer.Slots[i].item.amount,
                                                newContainer.Slots[i].parentId,
                                                newContainer.Slots[i].slotId,
                                                newContainer.Slots[i].allowedItems);
            }
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }
    public InventorySlot FindItemOnInventory(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].id == _item.id)
            {
                return GetSlots[i];
            }
        }
        return null;
    }
    public InventorySlot FindItemOnInventory(int _id)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].id == _id)
            {
                return GetSlots[i];
            }
        }
        return null;
    }
    public bool IsExistSameItemOnInventory(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].id == _item.id)
            {
                return true;
            }
        }
        return false;
    }
    public bool IsInventoryFull()
    {
        for (int i = 0; i < Container.Slots.Length; i++)
        {
            if (GetSlots[i].id == -1)
            {
                return false;
            }
        }
        return true;
    }
    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].id == -1)
                    counter++;
            }
            return counter;
        }
        
    }
    public int FindLastEmptySlotNumber()
    {
        for (int i = 0; i<GetSlots.Length; i++)
        {
            if (GetSlots[i].id == -1)
                return i;
        }
        return -1;
    }
}