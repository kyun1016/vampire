using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponJsonData
{
    public bool Enable;
    public int HUDBtnItemSpriteId;
    public int SpriteId;
    public Enum.PrefabType PrefabType;
    public Enum.WeaponType WeaponType;
    public Enum.WeaponTag WeaponTag;
    public Enum.DescType DescType;
    public int[] Projectile;
    public float[] ProjectileSize;
    public float[] Damage;
    public float[] Speed;

    // Melee
    public float[] Range;
    // Range
    public float[] CoolTime;
    public int[] Pierce;   // Range Weapon Only
}
