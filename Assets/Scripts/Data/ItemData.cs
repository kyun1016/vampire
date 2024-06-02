using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemData
{
    public int Id;
    public Enum.ItemType ItemType;
    public string Name;
    [TextArea]
    public string Desc;
    public int[] Projectile;
    public int[] Pierce;   // Range Weapon Only
    public float[] Damage;
    public float[] Speed;
    public float[] Info1;   // Range Weapon: CoolTime / Melee Weapon: Range
}
