using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItems : ItemObject
{
    [SerializeField]
    protected ConsumeType consumeType;
    public virtual void Use() { }
}
public enum ConsumeType
{
    Potion,
    Food,
    Pill,
    Aid
}
