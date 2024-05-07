using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemBuff 
{
    public Attributes attributes;
    public int value;
    public int minValue;
    public int maxValue;
    
    public ItemBuff(int _minValue,int _maxValue)
    {
        minValue= _minValue;
        maxValue= _maxValue;
        GenerateValue();
    }
    public void GenerateValue()
    {
        value = Random.Range(minValue, maxValue);
    }
}
public enum Attributes
{
    AttackDamage,
    AbilityDamage,
    Armor,
    MagicResist,
    Health,
    Mana,
    LifeSteal,
    CriticalStrikeChance,
    CriticalStrikeDamage,
    BaseManaRegen,
    BaseHealthRegen,
    MoveSpeed,
    AbilityHaste,
    AttackSpeed,
    MagicPenetration,
    ArmorPenetration,
    Lethality,
    Tenacity,
    HealAndShieldPower,
    GoldPerTenSecond,
}