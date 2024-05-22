using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shoe", menuName = "ItemObject/WearableItems/Shoe")]
public class Shoe : WearableItems
{
    private void Awake()
    {
        //itemType = ItemType.WearableItems;
        itemType = Items.Shoe;
        wearableType = WearableType.Shoe;
    }
}
