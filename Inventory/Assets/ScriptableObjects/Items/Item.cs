using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public int id;
    public int amount;
    public string Name;
    public ItemObject itemObject;
    public ItemBuff[] buffs;

    public Item()
    {
        this.id = -1;
        this.amount=0;
        this.Name=string.Empty;
        this.itemObject= null;
    }
    public Item(int _id,int _amount,string _name,ItemObject _itemObject)
    {
        this.id = _id;
        this.amount=_amount;
        this.Name = _name;
        this.itemObject= _itemObject;
    }
    public Item(int _id, int _amount, ItemObject _itemObject)
    {
        this.id = _id;
        this.amount = _amount;
        this.itemObject = _itemObject;
        this.Name=_itemObject.Name;
    }
    public void UpdateItem(Item _item)
    {
        this.id= _item.id;
        this.amount=_item.amount;
        this.itemObject = _item.itemObject;
        this.Name = _item.itemObject.Name;
    }
    public Item(ItemObject _itemObject)
    {
        this.itemObject= _itemObject;
        this.id= _itemObject.id;
        this.Name= _itemObject.Name;  
        this.buffs=new ItemBuff[_itemObject.itemBuffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(_itemObject.itemBuffs[i].minValue, _itemObject.itemBuffs[i].maxValue)
            {
                attributes = _itemObject.itemBuffs[i].attributes
            };
        }
    }
}
