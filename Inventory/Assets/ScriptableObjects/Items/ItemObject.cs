using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemObject : ScriptableObject
{
    public string Name;
    public int id;
    public bool isStackable;
    public int slotAmountMax;
    public Sprite uiDisplay;
    [SerializeField]
    public Items itemType;
    [TextArea(15, 20)]
    public string description;

    public ItemBuff[] itemBuffs;
}
