using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class FuncPool
{
    public static void SpawnEnemy(int level)
    {
        GameObject enemy = GameManager.instance.mEnemyPool.Get();
        enemy.transform.position = GameManager.instance.mPlayer.transform.position;
        Vector3 pos = Random.insideUnitCircle;
        if (pos.magnitude == 0)
            pos.x = 1;
        pos = pos.normalized;
        enemy.transform.position += pos * 20;
        enemy.GetComponent<Enemy>().Init(level);
    }

    public static void SpawnFieldObject()
    {
        GameObject item = GameManager.instance.mFieldObjectPool.Get();
        item.transform.position = GameManager.instance.mPlayer.transform.position;
        Vector3 pos = Random.insideUnitCircle;
        if (pos.magnitude == 0)
            pos.x = 1;
        pos = pos.normalized;
        item.transform.position += pos * 20;
        int value = Random.Range(0, GameManager.instance.mFieldObjectSprite.Length);
        item.GetComponent<FieldObject>().Init(value);
    }

    public static void DropGold(int value, Vector3 pos)
    {
        GameObject item = GameManager.instance.mDropPool.Get();
        item.transform.position = pos;
        item.transform.position += new Vector3(Random.value * 2 - 1, Random.value * 2 - 1);
        item.transform.rotation = Quaternion.identity;
        item.GetComponent<DropItem>().mData = value;
        switch (value)
        {
            case > 50:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Gold2;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Gold2];
                break;
            case > 10:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Gold1;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Gold1];
                break;
            default:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Gold0;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Gold0];
                break;
        }
        item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

    public static void DropExp(int value, Vector3 pos)
    {
        GameObject item = GameManager.instance.mDropPool.Get();
        item.transform.position = pos;
        item.transform.rotation = Quaternion.identity;
        item.GetComponent<DropItem>().mData = value;
        switch (value)
        {
            case > 50:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Exp2;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Exp2];
                break;
            case > 10:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Exp1;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Exp1];
                break;
            default:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Exp0;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Exp0];
                break;
        }
        item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

    public static void DropItem(Vector3 pos)
    {
        GameObject item = GameManager.instance.mDropPool.Get();
        item.transform.position = pos;
        item.transform.rotation = Quaternion.identity;

        float val = Random.value;
        switch (val)
        {
            case < 0.1f:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Mag;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Mag];
                break;
            case < 0.3f:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Health;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Health];
                break;
        }
    }

    //public static void PlacementGarlic(int id)
    //{
    //    Transform melee;
    //    if (GameManager.instance.mPlayer.mWeaponPool[id].mPool.Count == 0)
    //        melee = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
    //    else
    //    {
    //        mWeaponPool.mPool[0].SetActive(true);
    //        melee = mWeaponPool.mPool[0].transform;
    //    }
    //    melee.localScale = Vector3.one * GameManager.instance.mWeaponLastData[mId].ProjectileSize;
    //    melee.localPosition = Vector3.zero;
    //    melee.localRotation = Quaternion.identity;
    //    melee.GetComponent<Melee>().Init(GameManager.instance.mWeaponLastData[mId].Damage, GameManager.instance.mWeaponLastData[mId].Damage, Enum.DebuffType.TicDamage);
    //}
    //public void PlacementCircle()
    //{
    //    for (int i = 0; i < GameManager.instance.mWeaponLastData[mId].Projectile; ++i)
    //    {
    //        Transform melee;

    //        if (i < mWeaponPool.mPool.Count)
    //        {
    //            mWeaponPool.mPool[i].SetActive(true);
    //            melee = mWeaponPool.mPool[i].transform;
    //        }
    //        else
    //        {
    //            melee = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
    //            melee.parent = mWeaponPool.transform;
    //        }

    //        melee.localPosition = Vector3.zero;
    //        melee.localRotation = Quaternion.identity;

    //        Vector3 rotVec = Vector3.forward * 360 * i / GameManager.instance.mWeaponLastData[mId].Projectile;
    //        melee.Rotate(rotVec);
    //        melee.Translate(melee.up * GameManager.instance.mWeaponLastData[mId].Range, Space.World);
    //        melee.GetComponent<Melee>().Init(GameManager.instance.mWeaponLastData[mId].Damage);
    //    }
    //}

    //void Fire()
    //{
    //    if (!GameManager.instance.mPlayer.mScanner.mNearestTarget)
    //        return;

    //    Vector3 targetPos = GameManager.instance.mPlayer.mScanner.mNearestTarget.position;
    //    Vector3 rootDir = targetPos - GameManager.instance.mPlayer.transform.position;
    //    rootDir = rootDir.normalized;

    //    // 방사형 공격 로직
    //    for (int i = 0; i < GameManager.instance.mWeaponLastData[mId].Projectile; ++i)
    //    {
    //        Vector3 dir = rootDir;
    //        if (GameManager.instance.mWeaponLastData[mId].Projectile != 1)
    //        {
    //            float rot = 30 * (i / ((float)GameManager.instance.mWeaponLastData[mId].Projectile - 1)) - 15;
    //            dir = Quaternion.Euler(0, 0, rot) * dir;
    //        }

    //        Transform bullet = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
    //        bullet.position = GameManager.instance.mPlayer.transform.position;
    //        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    //        bullet.GetComponent<Range>().Init(Mathf.RoundToInt(GameManager.instance.mWeaponLastData[mId].Damage), GameManager.instance.mWeaponLastData[mId].Pierce, dir, GameManager.instance.mWeaponLastData[mId].Speed); // -1 is Infinity Per
    //    }

    //    GameManager.instance.PlaySFX(Enum.SFX.Range);
    //}
    //void FireWeb()
    //{
    //    if (!GameManager.instance.mPlayer.mScanner.mNearestTarget)
    //        return;

    //    Vector3 targetPos = GameManager.instance.mPlayer.mScanner.mNearestTarget.position;
    //    Vector3 rootDir = targetPos - GameManager.instance.mPlayer.transform.position;
    //    rootDir = rootDir.normalized;

    //    // 방사형 공격 로직
    //    for (int i = 0; i < GameManager.instance.mWeaponLastData[mId].Projectile; ++i)
    //    {
    //        Vector3 dir = rootDir;
    //        if (GameManager.instance.mWeaponLastData[mId].Projectile != 1)
    //        {
    //            float rot = 30 * (i / ((float)GameManager.instance.mWeaponLastData[mId].Projectile - 1)) - 15;
    //            dir = Quaternion.Euler(0, 0, rot) * dir;
    //        }

    //        Transform bullet = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
    //        bullet.position = GameManager.instance.mPlayer.transform.position;
    //        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    //        bullet.localScale = new Vector3(0.02f, 0.4f, 1f);
    //        bullet.GetComponent<Range>().Init(Mathf.RoundToInt(GameManager.instance.mWeaponLastData[mId].Damage), GameManager.instance.mWeaponLastData[mId].Pierce, dir, GameManager.instance.mWeaponLastData[mId].Speed, 0.2f, Enum.DebuffType.SlowCoef, Enum.EffectType.Stop); // -1 is Infinity Per
    //    }
    //    GameManager.instance.PlaySFX(Enum.SFX.Range);
    //}

    //void FireDagger()
    //{
    //    Transform bullet = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
    //    bullet.position = GameManager.instance.mPlayer.transform.position;
    //    bullet.rotation = Quaternion.FromToRotation(Vector3.up, GameManager.instance.mPlayer.mLastDir);
    //    bullet.GetComponent<Range>().Init(Mathf.RoundToInt(GameManager.instance.mWeaponLastData[mId].Damage), GameManager.instance.mWeaponLastData[mId].Pierce, GameManager.instance.mPlayer.mLastDir, GameManager.instance.mWeaponLastData[mId].Speed);

    //    GameManager.instance.PlaySFX(Enum.SFX.Range);
    //}
    //void FireAxe()
    //{
    //    Vector3 dir;
    //    if (mShotCnt % 2 == 0)
    //    {
    //        dir.x = 1;
    //        dir.y = 2;
    //        dir.z = 0;
    //    }
    //    else
    //    {
    //        dir.x = -1;
    //        dir.y = 2;
    //        dir.z = 0;
    //    }
    //    dir = dir.normalized;

    //    Transform bullet = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
    //    bullet.position = GameManager.instance.mPlayer.transform.position;
    //    bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    //    bullet.GetComponent<Range>().Init(Mathf.RoundToInt(GameManager.instance.mWeaponLastData[mId].Damage), GameManager.instance.mWeaponLastData[mId].Pierce, dir, GameManager.instance.mWeaponLastData[mId].Speed);

    //    GameManager.instance.PlaySFX(Enum.SFX.Range);
    //}


}
