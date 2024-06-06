using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponData
{
    public Enum.WeaponType WeaponType;
    public int Projectile;
    public float ProjectileSize;
    public float Damage;
    public float Speed;
    // Melee
    public float Range;
    // Range
    public float CoolTime;
    public int Pierce;
}
