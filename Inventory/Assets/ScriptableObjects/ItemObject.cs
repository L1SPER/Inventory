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

public class ItemObject : ScriptableObject
{
    public string Name;
    public int id;
    public bool isStackable;
    public Sprite prefab;
    [SerializeField]
    protected ItemType itemType;
    [TextArea(15, 20)]
    public string description;
}


