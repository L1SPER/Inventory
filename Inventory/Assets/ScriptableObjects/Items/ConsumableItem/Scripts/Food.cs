using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "ItemObject/ConsumableItems/Food")]
public class Food : ConsumableItems
{
    private void Awake()
    {
        //itemType = ItemType.ConsumableItems;
        consumeType = ConsumeType.Food;
    }
    public override void Use()
    {
        base.Use();
    }
}
