using System;
using System.Collections;
using System.Collections.Generic;
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

    public void AddItem(Item item)
    {
        if(!IsInventoryFull())
        {
            if (!database.GetItem[item.id].isStackable)
            {
                Container.Items[FindLastEmptySlotNumber()].UpdateSlot(item, item.id, item.amount);
            }
            else if (database.GetItem[item.id].isStackable)
            {
                for (int i = 0; i < Container.Items.Length; i++)
                {
                    //Envanterde item zaten var mý diye itemi arýyorum
                    if (Container.Items[i].id==item.id)
                    {
                        //Envanterde olan iteme ekledim.
                        if(database.GetItem[item.id].slotAmountMax >= item.amount + Container.Items[i].amount)
                        {
                            Container.Items[i].amount+= item.amount;
                        }
                        //Envanterde olmayan itemi ekledim.
                        else
                        {
                            int remain = item.amount + Container.Items[i].amount - database.GetItem[item.id].slotAmountMax;
                            Container.Items[i].amount = database.GetItem[item.id].slotAmountMax;

                            //Fazlasýný envanterde yer varsa slot oluþturdum yoksa itemin miktarýný azalttým
                            if(!IsInventoryFull())
                            {
                                //Arada bir yerde eðer boþ slot varsa diye son boþ slota koyuyorum
                                Container.Items[FindLastEmptySlotNumber() ].UpdateSlot(item, item.id, remain);
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
                        Container.Items[FindLastEmptySlotNumber()].UpdateSlot(item, item.id, item.amount);
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
    public void SwapItem(InventorySlot item1,InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.item,item2.amount,item2.id);
        item2.UpdateSlot(item1.item, item1.id, item1.amount);
        item1.UpdateSlot(temp.item, temp.id, temp.amount);
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
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if(newContainer.Items[i].item == null)
                    break;
                Container.Items[i].UpdateSlot(newContainer.Items[i].item,
                                                newContainer.Items[i].item.id,
                                                newContainer.Items[i].item.amount);
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
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].id == _item.id)
            {
                return Container.Items[i];
            }
        }
        return null;
    }
    public bool IsExistSameItemOnInventory(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].id == _item.id)
            {
                return true;
            }
        }
        return false;
    }
    public bool IsInventoryFull()
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].id == -1)
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
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if (Container.Items[i].id == -1)
                    counter++;
            }
            return counter;
        }
        
    }
    public int FindLastEmptySlotNumber()
    {
        for (int i = 0; i<Container.Items.Length; i++)
        {
            if (Container.Items[i].id == -1)
                return i;
        }
        return -1;
    }
}
[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[30];
    public void Clear()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].RemoveItem();
        }
    }
} 
[System.Serializable]
public class InventorySlot
{
    public ItemType[] allowedItems = new ItemType[0]; 
    public Item item=new Item();
    [System.NonSerialized]
    public UserInterface parent;
    public int parentId;
    public int id;
    public int amount;
    public InventorySlot()
    {
        this.item=new Item();
        this.id = -1;
        this.amount=0;
    }
    public InventorySlot(Item _item,int _amount, int _id)
    {
        this.item = _item;
        this.amount = _amount;
        this.id = _id;
    }
    //public InventorySlot(Item _item, int _amount, int _id, int _parentId)
    //{
    //    this.item = _item;
    //    this.amount = _amount;
    //    this.id = _id;
    //    this.parentId = _parentId;
    //}
    public void UpdateSlot(Item _item,int _id, int _amount)
    {
        this.item = _item;
        this.amount = _amount;
        this.id = _id;
    }
    public void UpdateSlot(Item _item, int _id, int _amount,int _parentId)
    {
        this.item = _item;
        this.amount = _amount;
        this.id = _id;
        this.parentId= _parentId;
    }
    public void AddAmount(int amount)
    {
        this.amount += amount;
    }
    public void RemoveItem()
    {
        this.item = new Item();
        this.id = -1;
        this.amount= 0;
    }
    public bool CanPlaceInSlot(ItemObject _item)
    {
        if(allowedItems.Length<=0)
        {
            return true;
        }
        for (int i = 0; i < allowedItems.Length; i++)
        {
            if (_item.itemType == allowedItems[i])
            {
                return true;
            }
        }
        return false;
    }
}
