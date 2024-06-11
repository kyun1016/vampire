using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : MonoBehaviour
{
    int mId;
    Enum.WeaponType mWeaponType;
    float mTime;
    int mShotCnt;
    bool mShotEn;
    PoolManager mWeaponPool;


    public void Init(int id)
    {
        if (id >= GameManager.instance.mPlayerData.WeaponSize)
            Debug.Assert(false, "Error");
        mId = id;

        // initialize Pool Manager (general setting)
        mWeaponPool = new GameObject("WeaponPool").AddComponent<PoolManager>();
        mWeaponPool.transform.parent = transform;
        mWeaponPool.transform.localPosition = Vector3.zero;
        mWeaponPool.transform.localRotation = Quaternion.identity;
        mWeaponPool.Init((int) GameManager.instance.mWeaponJsonData[GameManager.instance.mWeaponCtrlData[mId].Id].PrefabType);
        mWeaponPool.mPool[0].GetComponent<SpriteRenderer>().sprite = GameManager.instance.mWeaponSprite[GameManager.instance.mWeaponJsonData[GameManager.instance.mWeaponCtrlData[mId].Id].SpriteId];

        mWeaponType = GameManager.instance.mWeaponJsonData[GameManager.instance.mWeaponCtrlData[mId].Id].WeaponType;

        // initialize Root Prefab (particular setting)
        switch (mWeaponType)
        {
            case Enum.WeaponType.Melee:
            case Enum.WeaponType.RangeDagger:
                mWeaponPool.mPool[0].GetComponent<BoxCollider2D>().size = new Vector2(0.4f, 1);
                break;
            case Enum.WeaponType.RangeBullet:
                mWeaponPool.mPool[0].GetComponent<BoxCollider2D>().size = new Vector2(0.4f, 0.4f);
                break;
            case Enum.WeaponType.RangeAxe:
                mWeaponPool.mPool[0].GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.7f);
                mWeaponPool.mPool[0].GetComponent<Rigidbody2D>().gravityScale = 1;
                break;
            case Enum.WeaponType.Garlic:
                mWeaponPool.mPool[0].GetComponent<CircleCollider2D>().radius = 0.5f;
                break;
            case Enum.WeaponType.Web:
                mWeaponPool.mPool[0].GetComponent<BoxCollider2D>().size = new Vector2(3, 3);
                break;
            case Enum.WeaponType.Boom:
                mWeaponPool.mPool[0].GetComponent<CircleCollider2D>().radius = 1;
                break;
            case Enum.WeaponType.ThrowBoom:
                mWeaponPool.mPool[0].GetComponent<CircleCollider2D>().radius = 1;
                break;
            default:
                Debug.Assert(false, "Error");
                break;
        }
    }

    public void updatePool()
    {
        mWeaponPool.mPool[0].transform.localScale = Vector3.one * GameManager.instance.mWeaponLastData[mId].ProjectileSize;

        for (int i=1; i < mWeaponPool.mPool.Count; ++i)
        {
            mWeaponPool.mPool[i].transform.localScale = mWeaponPool.mPool[0].transform.localScale;
        }

        switch (mWeaponType)
        {
            case Enum.WeaponType.Melee:
                PlacementCircle();
                break;
            case Enum.WeaponType.Garlic:
                PlacementGarlic();
                break;
            case Enum.WeaponType.RangeBullet:
            case Enum.WeaponType.RangeDagger:
            case Enum.WeaponType.RangeAxe:
            case Enum.WeaponType.Web:
            case Enum.WeaponType.Boom:
            case Enum.WeaponType.ThrowBoom:
                break;
            default:
                Debug.Assert(false, "Error");
                break;
        }
    }

    void Update()
    {
        if (!GameManager.instance.mIsLive)
            return;

        switch (mWeaponType)
        {
            case Enum.WeaponType.Melee:
                transform.Rotate(Vector3.back * GameManager.instance.mWeaponLastData[mId].Speed * Time.deltaTime);
                break;
            case Enum.WeaponType.RangeBullet:
            case Enum.WeaponType.Web:
                mTime += Time.deltaTime;
                if (mTime > GameManager.instance.mWeaponLastData[mId].CoolTime)
                {
                    switch (mWeaponType)
                    {
                        case Enum.WeaponType.RangeBullet:
                            Fire();
                            break;
                        case Enum.WeaponType.Web:
                            FireWeb();
                            break;
                        default:
                            Debug.Assert(false, "Error");
                            break;
                    }
                    mTime = 0f;
                }
                break;
            case Enum.WeaponType.RangeDagger:
            case Enum.WeaponType.RangeAxe:
                mTime += Time.deltaTime;
                if (mTime > GameManager.instance.mWeaponLastData[mId].CoolTime)
                {
                    if (!mShotEn)
                    {
                        mTime = 0f;
                        mShotEn = true;
                        mShotCnt = 0;
                    }
                    else
                    {
                        switch (mWeaponType)
                        {
                            case Enum.WeaponType.RangeDagger:
                                FireDagger();
                                break;
                            case Enum.WeaponType.RangeAxe:
                                FireAxe();
                                break;
                            default:
                                Debug.Assert(false, "Error");
                                break;
                        }
                        ++mShotCnt;
                        if (mShotCnt == GameManager.instance.mWeaponLastData[mId].Projectile)
                        {
                            mShotEn = false;
                            mTime = 0f;
                        }
                        else
                        {
                            mTime = GameManager.instance.mWeaponLastData[mId].CoolTime - 0.1f;
                        }
                    }
                }
                break;
            case Enum.WeaponType.Boom:
            case Enum.WeaponType.ThrowBoom:
            case Enum.WeaponType.Garlic:
                break;
            default:
                Debug.Assert(false, "Error");
                break;
        }
    }

    public void PlacementGarlic()
    {
        Transform melee;
        if (mWeaponPool.mPool.Count == 0)
            melee = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
        else
        {
            mWeaponPool.mPool[0].SetActive(true);
            melee = mWeaponPool.mPool[0].transform;
        }
        melee.localScale = Vector3.one * GameManager.instance.mWeaponLastData[mId].ProjectileSize;
        melee.localPosition = Vector3.zero;
        melee.localRotation = Quaternion.identity;
        melee.GetComponent<Melee>().Init(GameManager.instance.mWeaponLastData[mId].Damage, GameManager.instance.mWeaponLastData[mId].Damage, Enum.DebuffType.TicDamage);
    }
    public void PlacementCircle()
    {
        for (int i = 0; i < GameManager.instance.mWeaponLastData[mId].Projectile; ++i)
        {
            Transform melee;

            if (i < mWeaponPool.mPool.Count)
            {
                mWeaponPool.mPool[i].SetActive(true);
                melee = mWeaponPool.mPool[i].transform;
            }
            else
            {
                melee = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
                melee.parent = mWeaponPool.transform;
            }

            melee.localPosition = Vector3.zero;
            melee.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / GameManager.instance.mWeaponLastData[mId].Projectile;
            melee.Rotate(rotVec);
            melee.Translate(melee.up * GameManager.instance.mWeaponLastData[mId].Range, Space.World);
            melee.GetComponent<Melee>().Init(GameManager.instance.mWeaponLastData[mId].Damage);
        }
    }

    void Fire()
    {
        if (!GameManager.instance.mPlayer.mScanner.mNearestTarget)
            return;

        Vector3 targetPos = GameManager.instance.mPlayer.mScanner.mNearestTarget.position;
        Vector3 rootDir = targetPos - GameManager.instance.mPlayer.transform.position;
        rootDir = rootDir.normalized;

        // 방사형 공격 로직
        for (int i = 0; i < GameManager.instance.mWeaponLastData[mId].Projectile; ++i)
        {
            Vector3 dir = rootDir;
            if (GameManager.instance.mWeaponLastData[mId].Projectile != 1)
            {
                float rot = 30 * (i / ((float)GameManager.instance.mWeaponLastData[mId].Projectile - 1)) - 15;
                dir = Quaternion.Euler(0, 0, rot) * dir;
            }

            Transform bullet = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
            bullet.position = GameManager.instance.mPlayer.transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<Range>().Init(Mathf.RoundToInt(GameManager.instance.mWeaponLastData[mId].Damage), GameManager.instance.mWeaponLastData[mId].Pierce, dir, GameManager.instance.mWeaponLastData[mId].Speed); // -1 is Infinity Per
        }

        GameManager.instance.PlaySFX(Enum.SFX.Range);
    }
    void FireWeb()
    {
        if (!GameManager.instance.mPlayer.mScanner.mNearestTarget)
            return;

        Vector3 targetPos = GameManager.instance.mPlayer.mScanner.mNearestTarget.position;
        Vector3 rootDir = targetPos - GameManager.instance.mPlayer.transform.position;
        rootDir = rootDir.normalized;

        // 방사형 공격 로직
        for (int i = 0; i < GameManager.instance.mWeaponLastData[mId].Projectile; ++i)
        {
            Vector3 dir = rootDir;
            if (GameManager.instance.mWeaponLastData[mId].Projectile != 1)
            {
                float rot = 30 * (i / ((float)GameManager.instance.mWeaponLastData[mId].Projectile - 1)) - 15;
                dir = Quaternion.Euler(0, 0, rot) * dir;
            }

            Transform bullet = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
            bullet.position = GameManager.instance.mPlayer.transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.localScale = new Vector3(0.02f, 0.4f, 1f);
            bullet.GetComponent<Range>().Init(Mathf.RoundToInt(GameManager.instance.mWeaponLastData[mId].Damage), GameManager.instance.mWeaponLastData[mId].Pierce, dir, GameManager.instance.mWeaponLastData[mId].Speed, 0.2f, Enum.DebuffType.SlowCoef, Enum.EffectType.Stop); // -1 is Infinity Per
        }
        GameManager.instance.PlaySFX(Enum.SFX.Range);
    }

    void FireDagger()
    {
        Transform bullet = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
        bullet.position = GameManager.instance.mPlayer.transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, GameManager.instance.mPlayer.mLastDir);
        bullet.GetComponent<Range>().Init(Mathf.RoundToInt(GameManager.instance.mWeaponLastData[mId].Damage), GameManager.instance.mWeaponLastData[mId].Pierce, GameManager.instance.mPlayer.mLastDir, GameManager.instance.mWeaponLastData[mId].Speed);

        GameManager.instance.PlaySFX(Enum.SFX.Range);
    }
    void FireAxe()
    {
        Vector3 dir;
        if(mShotCnt % 2 == 0)
        {
            dir.x = 1;
            dir.y = 2;
            dir.z = 0;
        }
        else
        {
            dir.x = -1;
            dir.y = 2;
            dir.z = 0;
        }
        dir = dir.normalized;

        Transform bullet = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
        bullet.position = GameManager.instance.mPlayer.transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Range>().Init(Mathf.RoundToInt(GameManager.instance.mWeaponLastData[mId].Damage), GameManager.instance.mWeaponLastData[mId].Pierce, dir, GameManager.instance.mWeaponLastData[mId].Speed);

        GameManager.instance.PlaySFX(Enum.SFX.Range);
    }
}
