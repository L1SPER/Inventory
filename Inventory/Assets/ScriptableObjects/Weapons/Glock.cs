using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Glock", menuName = "ItemObject/Weapons/Glock")]
public class Glock : Weapons
{
    private void Awake()
    {
        itemType = ItemType.Weapons;
        weaponType = WeaponType.RangedWeapon;
        weapon = Weapon.Glock;
    }

    public readonly int ammoClip;
    public readonly int damage;
    public readonly float accuracy;
    public readonly float attackSpeed;

    public int AmmoClipSize;
    public int AmmoInClip;
    public int AmmoOutsideOfClip;
    public int TotalAmmo { get { return this.AmmoInClip + this.AmmoOutsideOfClip; } }
    public double LastTimeWhenFired { get; protected set; } = double.NegativeInfinity;
    public double TimeSinceFired => Time.timeAsDouble - this.LastTimeWhenFired;

    public int bulletsPerTap;
    public float range, reloadTime;
    //bools
    public bool shooting, readyToShoot, reloading, canShootMultipleBullets;
}
