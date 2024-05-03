using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Knife", menuName = "ItemObject/Weapons/Knife")]
public class Knife : WeaponItems
{
    private void Awake()
    {
        itemType = ItemType.Weapons;
        weaponType = WeaponType.MeleeWeapon;
        weapon = Weapon.Knife;
    }

    public float attackDamage;
    public float attackSpeed;
    public float timeBetweenShooting, range, timeBetweenShots;
    //bools
    public bool shooting, readyToShoot;
}
