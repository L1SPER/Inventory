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
    public ItemBuff[] buffs=new ItemBuff[0];

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
        UpdateItemBuffs(_itemObject.itemBuffs);
    }
    public Item(int _id, int _amount, ItemObject _itemObject)
    {
        this.id = _id;
        this.amount = _amount;
        this.itemObject = _itemObject;
        this.Name=_itemObject.Name;
        UpdateItemBuffs(_itemObject.itemBuffs);
    }
    public Item(ItemObject _itemObject)
    {
        this.itemObject = _itemObject;
        this.id = _itemObject.id;
        this.Name = _itemObject.Name;
        this.buffs = new ItemBuff[_itemObject.itemBuffs.Length];
        UpdateItemBuffs(_itemObject.itemBuffs);
    }
    public void UpdateItem(Item _item)
    {
        this.id= _item.id;
        this.amount=_item.amount;
        this.itemObject = _item.itemObject;
        this.Name = _item.itemObject.Name;
    }
    public void UpdateItemBuffs(ItemBuff[] _itemBuffs)
    {
        if (_itemBuffs.Length <= 0) return;
        buffs=new ItemBuff[_itemBuffs.Length];
        for (int i = 0; i < _itemBuffs.Length; i++)
        {
            buffs[i]=new ItemBuff(_itemBuffs[i].minValue, _itemBuffs[i].maxValue);
            {
                buffs[i].attributes= _itemBuffs[i].attributes;
            };
            //this.buffs[i] = _itemBuffs[i];
            //this.buffs[i].attributes = _itemBuffs[i].attributes;
        }
    }
}
