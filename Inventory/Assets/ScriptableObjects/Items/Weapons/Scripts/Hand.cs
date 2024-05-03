using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hand", menuName = "ItemObject/Weapons/Hand")]
public class Hand : WeaponItems
{
    private void Awake()
    {
        itemType = ItemType.Weapons;
        weaponType = WeaponType.MeleeWeapon;
        weapon = Weapon.Hand;
    }

    public float attackDamage;
    public float attackSpeed;
    public float timeBetweenShooting, range, timeBetweenShots;
    //bools
    public bool shooting, readyToShoot;
}
