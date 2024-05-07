using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    WearableItems,
    Weapons,
    ConsumableItems,
}
[System.Serializable]
public class ItemObject : ScriptableObject
{
    public string Name;
    public int id;
    public bool isStackable;
    public int slotAmountMax;
    public Sprite uiDisplay;
    [SerializeField]
    public ItemType itemType;
    [TextArea(15, 20)]
    public string description;
}
