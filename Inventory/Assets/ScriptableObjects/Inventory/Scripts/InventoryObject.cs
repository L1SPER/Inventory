using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "ItemObject/InventoryObject")]
public class InventoryObject : ScriptableObject
{
    public Inventory inventory;
    public void AddItem(Item item)
    {
        if(!IsInventoryFull())
        {
            if (!item.itemObject.isStackable)
            {
                inventory.Items[GetLastEmptySlot()].UpdateSlot(item, item.id, item.amount);
                Destroy(item.gameObject);
            }
            else if (item.itemObject.isStackable)
            {
                for (int i = 0; i < inventory.Items.Length; i++)
                {
                    //Envanterde item zaten var mý diye itemi arýyorum
                    if (inventory.Items[i].id==item.id)
                    {
                        //Envanterde olan iteme ekledim.
                        if(item.itemObject.slotAmountMax >= item.amount + inventory.Items[i].amount)
                        {
                            inventory.Items[i].amount+= item.amount;
                            Destroy(item.gameObject);
                        }
                        //Envanterde olmayan itemi ekledim.
                        else
                        {
                            int remain = item.amount + inventory.Items[i].amount - item.itemObject.slotAmountMax;
                            inventory.Items[i].amount = item.itemObject.slotAmountMax;

                            //Fazlasýný envanterde yer varsa slot oluþturdum yoksa itemin miktarýný azalttým
                            if(!IsInventoryFull())
                            {
                                //Arada bir yerde eðer boþ slot varsa diye son boþ slota koyuyorum
                                inventory.Items[GetLastEmptySlot()].UpdateSlot(item, item.id, remain);
                                Destroy(item.gameObject);
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
                        inventory.Items[GetLastEmptySlot()].UpdateSlot(item, item.id, item.amount);
                        Destroy(item.gameObject);
                        break;
                    }
                }
            }
        }
    }
    public InventorySlot FindItemInSlots(Item _item)
    {
        for (int i = 0; i < inventory.Items.Length; i++)
        {
            if (inventory.Items[i].id == _item.id)
            {
                return inventory.Items[i];
            }
        }
        return null;
    }
    public bool IsExistSameItemInSlots(Item _item)
    {
        for (int i = 0; i < inventory.Items.Length; i++)
        {
            if (inventory.Items[i].id == _item.id)
            {
                return true;
            }
        }
        return false;
    }
    public bool IsInventoryFull()
    {
        for (int i = 0; i < inventory.Items.Length; i++)
        {
            if (inventory.Items[i].id == -1)
            {
                return false;
            }
        }
        return true;
    }
    public int GetLastEmptySlot()
    {
        for (int i = 0; i < inventory.Items.Length; i++)
        {
            if (inventory.Items[i].id == -1)
                return i;
        }
        return -1;
    }
}
[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[30];
}
[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int id;
    public int amount;
    public InventorySlot()
    {
        this.item = null;
        this.id = -1;
        this.amount = 0;
    }
    public InventorySlot(Item _item,int _amount, int _id)
    {
        this.item = _item;
        this.amount = _amount;
        this.id = _id;
    }
    public void UpdateSlot(Item _item,int _id, int _amount)
    {
        this.item = _item;
        this.amount = _amount;
        this.id = _id;
    }
    public void AddAmount(int amount)
    {
        this.amount += amount;
    }
}
