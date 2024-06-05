using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class FuncWeapon
{
    public static void ClearPerk()
    {
        GameManager.instance.mPerkData.Projectile = 0;
        GameManager.instance.mPerkData.Damage = 0;
        GameManager.instance.mPerkData.Speed = 0;
        // Melee
        GameManager.instance.mPerkData.Range = 0;
        // Range
        GameManager.instance.mPerkData.CoolTime = 0;
        GameManager.instance.mPerkData.Pierce = 0;

        // Coeffcient
        GameManager.instance.mPerkData.ProjectileCoef = 1;
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

        GameManager.instance.mPerkData.Projectile += GameManager.instance.mPerkJsonData[id].Projectile[level];
        GameManager.instance.mPerkData.Damage += GameManager.instance.mPerkJsonData[id].Damage[level];
        GameManager.instance.mPerkData.Speed += GameManager.instance.mPerkJsonData[id].Speed[level];
        GameManager.instance.mPerkData.Range += GameManager.instance.mPerkJsonData[id].Range[level];
        GameManager.instance.mPerkData.CoolTime += GameManager.instance.mPerkJsonData[id].CoolTime[level];
        GameManager.instance.mPerkData.Pierce += GameManager.instance.mPerkJsonData[id].Pierce[level];
        GameManager.instance.mPerkData.ProjectileCoef += GameManager.instance.mPerkJsonData[id].ProjectileCoef[level];
        GameManager.instance.mPerkData.DamageCoef += GameManager.instance.mPerkJsonData[id].DamageCoef[level];
        GameManager.instance.mPerkData.SpeedCoef += GameManager.instance.mPerkJsonData[id].SpeedCoef[level];
        GameManager.instance.mPerkData.RangeCoef += GameManager.instance.mPerkJsonData[id].RangeCoef[level];
        GameManager.instance.mPerkData.CoolTimeCoef += GameManager.instance.mPerkJsonData[id].CoolTimeCoef[level];
        GameManager.instance.mPerkData.PierceCoef += GameManager.instance.mPerkJsonData[id].PierceCoef[level];

        if (level != 0)
        {
            --level;
            GameManager.instance.mPerkData.Projectile -= GameManager.instance.mPerkJsonData[id].Projectile[level];
            GameManager.instance.mPerkData.Damage -= GameManager.instance.mPerkJsonData[id].Damage[level];
            GameManager.instance.mPerkData.Speed -= GameManager.instance.mPerkJsonData[id].Speed[level];
            GameManager.instance.mPerkData.Range -= GameManager.instance.mPerkJsonData[id].Range[level];
            GameManager.instance.mPerkData.CoolTime -= GameManager.instance.mPerkJsonData[id].CoolTime[level];
            GameManager.instance.mPerkData.Pierce -= GameManager.instance.mPerkJsonData[id].Pierce[level];
            GameManager.instance.mPerkData.ProjectileCoef -= GameManager.instance.mPerkJsonData[id].ProjectileCoef[level];
            GameManager.instance.mPerkData.DamageCoef -= GameManager.instance.mPerkJsonData[id].DamageCoef[level];
            GameManager.instance.mPerkData.SpeedCoef -= GameManager.instance.mPerkJsonData[id].SpeedCoef[level];
            GameManager.instance.mPerkData.RangeCoef -= GameManager.instance.mPerkJsonData[id].RangeCoef[level];
            GameManager.instance.mPerkData.CoolTimeCoef -= GameManager.instance.mPerkJsonData[id].CoolTimeCoef[level];
            GameManager.instance.mPerkData.PierceCoef -= GameManager.instance.mPerkJsonData[id].PierceCoef[level];
        }
    }

    public static void InitPerkIdx(int idx)
    {
        if (idx >= GameManager.instance.mPlayerData.WeaponSize)
            Debug.Assert(false, "Error");

        int id = GameManager.instance.mPerkCtrlData[idx].Id;
        int level = GameManager.instance.mPerkCtrlData[idx].Level;

        GameManager.instance.mPerkData.Projectile += GameManager.instance.mPerkJsonData[id].Projectile[level];
        GameManager.instance.mPerkData.Damage += GameManager.instance.mPerkJsonData[id].Damage[level];
        GameManager.instance.mPerkData.Speed += GameManager.instance.mPerkJsonData[id].Speed[level];
        GameManager.instance.mPerkData.Range += GameManager.instance.mPerkJsonData[id].Range[level];
        GameManager.instance.mPerkData.CoolTime += GameManager.instance.mPerkJsonData[id].CoolTime[level];
        GameManager.instance.mPerkData.Pierce += GameManager.instance.mPerkJsonData[id].Pierce[level];
        GameManager.instance.mPerkData.ProjectileCoef += GameManager.instance.mPerkJsonData[id].ProjectileCoef[level];
        GameManager.instance.mPerkData.DamageCoef += GameManager.instance.mPerkJsonData[id].DamageCoef[level];
        GameManager.instance.mPerkData.SpeedCoef += GameManager.instance.mPerkJsonData[id].SpeedCoef[level];
        GameManager.instance.mPerkData.RangeCoef += GameManager.instance.mPerkJsonData[id].RangeCoef[level];
        GameManager.instance.mPerkData.CoolTimeCoef += GameManager.instance.mPerkJsonData[id].CoolTimeCoef[level];
        GameManager.instance.mPerkData.PierceCoef += GameManager.instance.mPerkJsonData[id].PierceCoef[level];
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

        GameManager.instance.mWeaponData[idx].WeaponType = GameManager.instance.mWeaponJsonData[id].WeaponType;
        GameManager.instance.mWeaponData[idx].Projectile = GameManager.instance.mWeaponJsonData[id].Projectile[level];
        GameManager.instance.mWeaponData[idx].Damage = GameManager.instance.mWeaponJsonData[id].Damage[level];
        GameManager.instance.mWeaponData[idx].Speed = GameManager.instance.mWeaponJsonData[id].Speed[level];
        GameManager.instance.mWeaponData[idx].Range = GameManager.instance.mWeaponJsonData[id].Range[level];
        GameManager.instance.mWeaponData[idx].CoolTime = GameManager.instance.mWeaponJsonData[id].CoolTime[level];
        GameManager.instance.mWeaponData[idx].Pierce = GameManager.instance.mWeaponJsonData[id].Pierce[level];
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
}
