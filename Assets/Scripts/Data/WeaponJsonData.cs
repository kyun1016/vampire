using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponJsonData
{
    public int SpriteId;
    public int PrefabId;
    public Enum.WeaponType WeaponType;
    public string Name;
    public Enum.DescType DescType;
    [TextArea]
    public string Desc;
    public int[] Projectile;
    public float[] Damage;
    public float[] Speed;

    // Melee
    public float[] Range;
    // Range
    public float[] CoolTime;
    public int[] Pierce;   // Range Weapon Only
}
