using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enum
{
    public enum Achive
    {
        UnlockChar2,
        UnlockChar3,
    }
    public enum SFX
    {
        Dead,
        Hit0,
        Hit1,
        LevelUp,
        Lose,
        Melee0,
        Melee1,
        Range,
        Select,
        Win
    }
    public enum PrefabType
    {
        Enemy,
        MeleeBox,
        MeleeCircle,
        RangeBox,
        RangeCircle,
        DropItem
    }
    public enum DropItemSprite
    {
        Exp0,
        Exp1,
        Exp2,
        Health,
        Mag
    }
    public enum WeaponTag
    {
        Bullet,
        Melee,
        SpiderWeb,
        Garlic
    }

    public enum WeaponType
    {
        Melee,
        RangeBullet,
        RangeDagger,
        RangeAxe,
        Garlic,
        Web,
        Boom,
        ThrowBoom
    }
    public enum DescType
    {
        Melee,
        Range,
        Perk,
        Heal
    }
}
