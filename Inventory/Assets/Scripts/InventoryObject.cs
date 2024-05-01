using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "ItemObject/InventoryObject")]
public class InventoryObject : ScriptableObject
{
    Inventory inventory;
    public void AddItem(Item item)
    {
        if(!IsInventoryFull())
        {
            if (!item.itemObject.isStackable)
            {
                inventory.Items[GetLastEmptySlot()].item = item;
                inventory.Items[GetLastEmptySlot()].id = item.id;
                inventory.Items[GetLastEmptySlot()].amount = item.amount;
            }
            else if (item.itemObject.isStackable)
            {
                for (int i = 0; i < inventory.Items.Length; i++)
                {
                    if (inventory.Items[i].id==item.id)
                    {
                        if(item.itemObject.slotAmountMax >= item.amount + inventory.Items[i].amount)
                        {
                            inventory.Items[i].amount+= item.amount;
                            Destroy(item.gameObject);
                        }
                        else
                        {
                            int remain = item.amount + inventory.Items[i].amount - item.itemObject.slotAmountMax;
                            inventory.Items[i].amount = item.itemObject.slotAmountMax;

                            //Fazlasýný envanterde yer varsa slot oluþturdum yoksa itemin miktarýný azalttým
                            if(!IsInventoryFull())
                            {
                                //Arada bir yerde eðer boþ slot varsa diye son boþ slota koyuyorum
                                inventory.Items[GetLastEmptySlot()].item = item;
                                inventory.Items[GetLastEmptySlot()].id = item.id;
                                inventory.Items[GetLastEmptySlot()].amount = remain;
                                Destroy(item.gameObject);
                            }
                            else
                            {
                                item.amount = remain;
                                Debug.Log("Envanter dolu");
                            }
                        }
                    }
                }
            }
        }
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
    public InventorySlot[] Items = new InventorySlot[25];
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
    public void UpdateSlot(Item _item,int _amount,int _id)
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
