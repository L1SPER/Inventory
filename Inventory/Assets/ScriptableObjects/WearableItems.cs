using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearableItems : ItemObject
{
    [SerializeField]
    protected WearableType wearableType;
}
public enum WearableType
{
    Helmet,
    ChestArmor,
    Glove,
    Trousers,
    Shoe,
    Amulet,
    Ring
}
