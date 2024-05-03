using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItems : ItemObject
{
    [SerializeField]
    protected WeaponType weaponType;
    [SerializeField]
    protected Weapon weapon;
}
public enum WeaponType
{
    MeleeWeapon,
    RangedWeapon
}
public enum Weapon
{
    Hand,
    Knife,
    Sword,
    Dagger,
    Shield,
    Arrow,
    Glock,
    Usp,
    Ak47,
    M4A1S,
    M4A4,
    AWP
}
