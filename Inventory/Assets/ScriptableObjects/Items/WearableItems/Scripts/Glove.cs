using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Glove", menuName = "ItemObject/WearableItems/Glove")]
public class Glove : WearableItems
{
    private void Awake()
    {
        itemType = ItemType.WearableItems;
        wearableType = WearableType.Glove;
    }
}
