using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "ItemObject/ConsumableItems/Potion")]
public class Potion : ConsumableItems
{
    private void Awake()
    {
        itemType = ItemType.ConsumableItems;
        consumeType = ConsumeType.Potion;
    }
    public override void Use()
    {
        base.Use();
    }
}
