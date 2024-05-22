using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ring", menuName = "ItemObject/WearableItems/Ring")]
public class Ring : WearableItems
{
    private void Awake()
    {
        //itemType = ItemType.WearableItems;
        itemType = Items.Ring;
        wearableType = WearableType.Ring;
    }
}
