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
    public enum Language
    {
        English,
        Korean
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
        DropItem,
        FieldObject
    }
    public enum DebuffType
    {
        None,
        TicDamage,
        Slow
    }
    public enum DropItemSprite
    {
        Exp0,
        Exp1,
        Exp2,
        Health,
        Mag,
        Gold0,
        Gold1,
        Gold2
    }
    public enum FieldObjectSprite
    {
        Box,
        Candle,
        Bush
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
