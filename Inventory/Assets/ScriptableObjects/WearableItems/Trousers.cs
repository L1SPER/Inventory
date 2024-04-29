using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trousers", menuName = "ItemObject/WearableItems/Trousers")]
public class Trousers : WearableItems
{
    private void Awake()
    {
        itemType = ItemType.WearableItems;
        wearableType = WearableType.Trousers;
    }
}
