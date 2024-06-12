using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class FuncWeapon
{
    public static void ClearPerk()
    {
        GameManager.instance.mPerkData.MovementSpeed = 0;
        GameManager.instance.mPerkData.Projectile = 0;
        GameManager.instance.mPerkData.ProjectileSize = 0;
        GameManager.instance.mPerkData.Damage = 0;
        GameManager.instance.mPerkData.Speed = 0;
        // Melee
        GameManager.instance.mPerkData.Range = 0;
        // Range
        GameManager.instance.mPerkData.CoolTime = 0;
        GameManager.instance.mPerkData.Pierce = 0;

        // Coeffcient
        GameManager.instance.mPerkData.MovementSpeedCoef = 1;
        GameManager.instance.mPerkData.ProjectileCoef = 1;
        GameManager.instance.mPerkData.ProjectileSizeCoef = 1;
        GameManager.instance.mPerkData.DamageCoef = 1;
        GameManager.instance.mPerkData.SpeedCoef = 1;
        // Melee
        GameManager.instance.mPerkData.RangeCoef = 1;
        // Range
        GameManager.instance.mPerkData.CoolTimeCoef = 1;
        GameManager.instance.mPerkData.PierceCoef = 1;
    }
    public static void UpdatePerkIdx(int idx)
    {
        if (idx >= GameManager.instance.mPlayerData.PerkSize)
            Debug.Assert(false, "Error");

        int id = GameManager.instance.mPerkCtrlData[idx].Id;
        int level = GameManager.instance.mPerkCtrlData[idx].Level;

        GameManager.instance.mPerkData.MovementSpeed += GameManager.instance.mJsonPerkData[id].MovementSpeed[level];
        GameManager.instance.mPerkData.Projectile += GameManager.instance.mJsonPerkData[id].Projectile[level];
        GameManager.instance.mPerkData.ProjectileSize+= GameManager.instance.mJsonPerkData[id].ProjectileSize[level];
        GameManager.instance.mPerkData.Damage += GameManager.instance.mJsonPerkData[id].Damage[level];
        GameManager.instance.mPerkData.Speed += GameManager.instance.mJsonPerkData[id].Speed[level];
        GameManager.instance.mPerkData.Range += GameManager.instance.mJsonPerkData[id].Range[level];
        GameManager.instance.mPerkData.CoolTime += GameManager.instance.mJsonPerkData[id].CoolTime[level];
        GameManager.instance.mPerkData.Pierce += GameManager.instance.mJsonPerkData[id].Pierce[level];
        GameManager.instance.mPerkData.MovementSpeedCoef += GameManager.instance.mJsonPerkData[id].MovementSpeedCoef[level];
        GameManager.instance.mPerkData.ProjectileCoef += GameManager.instance.mJsonPerkData[id].ProjectileCoef[level];
        GameManager.instance.mPerkData.ProjectileSizeCoef += GameManager.instance.mJsonPerkData[id].ProjectileSizeCoef[level];
        GameManager.instance.mPerkData.DamageCoef += GameManager.instance.mJsonPerkData[id].DamageCoef[level];
        GameManager.instance.mPerkData.SpeedCoef += GameManager.instance.mJsonPerkData[id].SpeedCoef[level];
        GameManager.instance.mPerkData.RangeCoef += GameManager.instance.mJsonPerkData[id].RangeCoef[level];
        GameManager.instance.mPerkData.CoolTimeCoef += GameManager.instance.mJsonPerkData[id].CoolTimeCoef[level];
        GameManager.instance.mPerkData.PierceCoef += GameManager.instance.mJsonPerkData[id].PierceCoef[level];

        if (level != 0)
        {
            --level;
            GameManager.instance.mPerkData.MovementSpeed -= GameManager.instance.mJsonPerkData[id].MovementSpeed[level];
            GameManager.instance.mPerkData.Projectile -= GameManager.instance.mJsonPerkData[id].Projectile[level];
            GameManager.instance.mPerkData.ProjectileSize -= GameManager.instance.mJsonPerkData[id].ProjectileSize[level];
            GameManager.instance.mPerkData.Damage -= GameManager.instance.mJsonPerkData[id].Damage[level];
            GameManager.instance.mPerkData.Speed -= GameManager.instance.mJsonPerkData[id].Speed[level];
            GameManager.instance.mPerkData.Range -= GameManager.instance.mJsonPerkData[id].Range[level];
            GameManager.instance.mPerkData.CoolTime -= GameManager.instance.mJsonPerkData[id].CoolTime[level];
            GameManager.instance.mPerkData.Pierce -= GameManager.instance.mJsonPerkData[id].Pierce[level];
            GameManager.instance.mPerkData.MovementSpeedCoef -= GameManager.instance.mJsonPerkData[id].MovementSpeedCoef[level];
            GameManager.instance.mPerkData.ProjectileCoef -= GameManager.instance.mJsonPerkData[id].ProjectileCoef[level];
            GameManager.instance.mPerkData.ProjectileSizeCoef -= GameManager.instance.mJsonPerkData[id].ProjectileSizeCoef[level];
            GameManager.instance.mPerkData.DamageCoef -= GameManager.instance.mJsonPerkData[id].DamageCoef[level];
            GameManager.instance.mPerkData.SpeedCoef -= GameManager.instance.mJsonPerkData[id].SpeedCoef[level];
            GameManager.instance.mPerkData.RangeCoef -= GameManager.instance.mJsonPerkData[id].RangeCoef[level];
            GameManager.instance.mPerkData.CoolTimeCoef -= GameManager.instance.mJsonPerkData[id].CoolTimeCoef[level];
            GameManager.instance.mPerkData.PierceCoef -= GameManager.instance.mJsonPerkData[id].PierceCoef[level];
        }
    }

    public static void InitPerkIdx(int idx)
    {
        if (idx >= GameManager.instance.mPlayerData.WeaponSize)
            Debug.Assert(false, "Error");

        int id = GameManager.instance.mPerkCtrlData[idx].Id;
        int level = GameManager.instance.mPerkCtrlData[idx].Level;

        GameManager.instance.mPerkData.MovementSpeed += GameManager.instance.mJsonPerkData[id].MovementSpeed[level];
        GameManager.instance.mPerkData.Projectile += GameManager.instance.mJsonPerkData[id].Projectile[level];
        GameManager.instance.mPerkData.ProjectileSize += GameManager.instance.mJsonPerkData[id].ProjectileSize[level];
        GameManager.instance.mPerkData.Damage += GameManager.instance.mJsonPerkData[id].Damage[level];
        GameManager.instance.mPerkData.Speed += GameManager.instance.mJsonPerkData[id].Speed[level];
        GameManager.instance.mPerkData.Range += GameManager.instance.mJsonPerkData[id].Range[level];
        GameManager.instance.mPerkData.CoolTime += GameManager.instance.mJsonPerkData[id].CoolTime[level];
        GameManager.instance.mPerkData.Pierce += GameManager.instance.mJsonPerkData[id].Pierce[level];
        GameManager.instance.mPerkData.MovementSpeedCoef += GameManager.instance.mJsonPerkData[id].MovementSpeedCoef[level];
        GameManager.instance.mPerkData.ProjectileCoef += GameManager.instance.mJsonPerkData[id].ProjectileCoef[level];
        GameManager.instance.mPerkData.ProjectileSizeCoef += GameManager.instance.mJsonPerkData[id].ProjectileSizeCoef[level];
        GameManager.instance.mPerkData.DamageCoef += GameManager.instance.mJsonPerkData[id].DamageCoef[level];
        GameManager.instance.mPerkData.SpeedCoef += GameManager.instance.mJsonPerkData[id].SpeedCoef[level];
        GameManager.instance.mPerkData.RangeCoef += GameManager.instance.mJsonPerkData[id].RangeCoef[level];
        GameManager.instance.mPerkData.CoolTimeCoef += GameManager.instance.mJsonPerkData[id].CoolTimeCoef[level];
        GameManager.instance.mPerkData.PierceCoef += GameManager.instance.mJsonPerkData[id].PierceCoef[level];
    }

    public static void InitPerkLoad()
    {
        ClearPerk();

        for (int i = 0; i < GameManager.instance.mPlayerData.PerkSize; ++i)
        {
            UpdatePerkIdx(i);
        }
    }
    public static void InitWeaponIdx(int idx)
    {
        if (idx >= GameManager.instance.mPlayerData.WeaponSize)
            Debug.Assert(false, "Error");

        int id = GameManager.instance.mWeaponCtrlData[idx].Id;
        int level = GameManager.instance.mWeaponCtrlData[idx].Level;

        GameManager.instance.mWeaponData[idx].WeaponType = GameManager.instance.mJsonWeaponData[id].WeaponType;
        GameManager.instance.mWeaponData[idx].Projectile = GameManager.instance.mJsonWeaponData[id].Projectile[level];
        GameManager.instance.mWeaponData[idx].ProjectileSize = GameManager.instance.mJsonWeaponData[id].ProjectileSize[level];
        GameManager.instance.mWeaponData[idx].Damage = GameManager.instance.mJsonWeaponData[id].Damage[level];
        GameManager.instance.mWeaponData[idx].Speed = GameManager.instance.mJsonWeaponData[id].Speed[level];
        GameManager.instance.mWeaponData[idx].Range = GameManager.instance.mJsonWeaponData[id].Range[level];
        GameManager.instance.mWeaponData[idx].CoolTime = GameManager.instance.mJsonWeaponData[id].CoolTime[level];
        GameManager.instance.mWeaponData[idx].Pierce = GameManager.instance.mJsonWeaponData[id].Pierce[level];
    }
    public static void InitWeaponLoad()
    {
        for (int i=0; i< GameManager.instance.mPlayerData.WeaponSize;++i)
        {
            InitWeaponIdx(i);
        }
    }
    public static void UpdateWeaponLastIdx(int idx)
    {
        if (idx >= GameManager.instance.mPlayerData.WeaponSize)
            Debug.Assert(false, "Error");

        GameManager.instance.mWeaponLastData[idx].WeaponType = GameManager.instance.mWeaponData[idx].WeaponType;
        GameManager.instance.mWeaponLastData[idx].Projectile = Mathf.RoundToInt((GameManager.instance.mWeaponData[idx].Projectile + GameManager.instance.mPerkData.Projectile) * GameManager.instance.mPerkData.ProjectileCoef);
        GameManager.instance.mWeaponLastData[idx].ProjectileSize = Mathf.RoundToInt((GameManager.instance.mWeaponData[idx].ProjectileSize + GameManager.instance.mPerkData.ProjectileSize) * GameManager.instance.mPerkData.ProjectileSizeCoef);
        GameManager.instance.mWeaponLastData[idx].Damage = Mathf.RoundToInt((GameManager.instance.mWeaponData[idx].Damage + GameManager.instance.mPerkData.Damage) * GameManager.instance.mPerkData.DamageCoef);
        GameManager.instance.mWeaponLastData[idx].Speed = Mathf.RoundToInt((GameManager.instance.mWeaponData[idx].Speed + GameManager.instance.mPerkData.Speed) * GameManager.instance.mPerkData.SpeedCoef);
        GameManager.instance.mWeaponLastData[idx].Range = Mathf.RoundToInt((GameManager.instance.mWeaponData[idx].Range + GameManager.instance.mPerkData.Range) * GameManager.instance.mPerkData.RangeCoef);
        GameManager.instance.mWeaponLastData[idx].CoolTime = Mathf.Clamp((GameManager.instance.mWeaponData[idx].CoolTime - GameManager.instance.mPerkData.CoolTime) * (1 / GameManager.instance.mPerkData.CoolTimeCoef), 0.05f, 100);
        GameManager.instance.mWeaponLastData[idx].Pierce = Mathf.RoundToInt((GameManager.instance.mWeaponData[idx].Pierce + GameManager.instance.mPerkData.Pierce) * GameManager.instance.mPerkData.PierceCoef);
    }
    public static void ReloadWeaponLast()
    {
        for (int i = 0; i < GameManager.instance.mPlayerData.WeaponSize; ++i)
        {
            UpdateWeaponLastIdx(i);
        }
    }
    public static void InitWeaponLastLoad()
    {
        ClearPerk();
        InitPerkLoad();
        InitWeaponLoad();

        for (int i = 0; i < GameManager.instance.mPlayerData.WeaponSize; ++i)
        {
            UpdateWeaponLastIdx(i);
        }
    }

    public static void UpdatePlayerMovement()
    {
        GameManager.instance.mPlayerData.MovementSpeed =
            (GameManager.instance.mJsonPlayerData[GameManager.instance.mPlayerData.Id].MovementSpeed +
            GameManager.instance.mPerkData.MovementSpeed) * GameManager.instance.mPerkData.MovementSpeedCoef;
    }
}
