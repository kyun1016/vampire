using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PerkData
{
    public int Projectile;
    public float Damage;
    public float Speed;
    // Melee
    public float Range;
    // Range
    public float CoolTime;
    public int Pierce;

    // Coeffcient
    public float ProjectileCoef;
    public float DamageCoef;
    public float SpeedCoef;
    // Melee
    public float RangeCoef;
    // Range
    public float CoolTimeCoef;
    public float PierceCoef;
}
