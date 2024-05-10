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
    public Item(ItemObject itemObject)
    {
        this.itemObject= itemObject;
        this.id= itemObject.id;
        this.Name= itemObject.Name;  
        this.buffs=new ItemBuff[itemObject.itemBuffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(itemObject.itemBuffs[i].minValue, itemObject.itemBuffs[i].maxValue)
            {
                attributes = itemObject.itemBuffs[i].attributes
            };
        }
    }
}
