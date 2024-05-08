using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour,IInteractable
{
    public ItemObject itemObject;
    public int id;
    public int amount;
    public string Name;
    public ItemBuff[] buffs;

    public Item()
    {
        //this.itemObject= null;
        this.id= -1;
        this.amount=0;
        //Name=string.Empty;
    }
    public Item(ItemObject itemObject)
    {
        this.id= itemObject.id;
        //Name= itemObject.name;  
        buffs=new ItemBuff[itemObject.itemBuffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(itemObject.itemBuffs[i].minValue, itemObject.itemBuffs[i].maxValue)
            {
                attributes = itemObject.itemBuffs[i].attributes
            };
        }
    }

    public void InteractWithoutPressingButton()
    {
        GetComponent<ItemInfo>().StartCoroutine("OpenCanvasByTime");
    }

    public void InteractWithPressingButton()
    {
        
    }

    private void Awake()
    {
        this.id = itemObject.id;
    }
}
