using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChestArmor", menuName = "ItemObject/WearableItems/ChestArmor")]
public class ChestArmor : WearableItems
{
    private void Awake()
    {
        //itemType = ItemType.WearableItems;
        itemType = Items.ChestArmor;
        wearableType = WearableType.ChestArmor;
    }
}
