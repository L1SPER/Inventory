using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Amulet", menuName = "ItemObject/WearableItems/Amulet")]
public class Amulet : WearableItems
{
    private void Awake()
    {
        //itemType = ItemType.WearableItems;
        itemType = Items.Amulet;
        wearableType = WearableType.Amulet;
    }
}
