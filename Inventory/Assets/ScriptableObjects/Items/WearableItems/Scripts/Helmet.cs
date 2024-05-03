using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Helmet", menuName = "ItemObject/WearableItems/Helmet")]
public class Helmet : WearableItems
{
    private void Awake()
    {
        itemType = ItemType.WearableItems;
        wearableType = WearableType.Helmet;
    }
}
