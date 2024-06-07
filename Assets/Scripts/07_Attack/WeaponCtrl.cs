using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : MonoBehaviour
{
    int mId;
    float mTime;
    bool mEnable;
    int mShotCnt;
    bool mShotEn;
    PoolManager mWeaponPool;

    public void Init(int id)
    {
        mEnable = true;
        if (id >= GameManager.instance.mPlayerData.WeaponSize)
            Debug.Assert(false, "Error");
        mId = id;

        mWeaponPool = new GameObject().AddComponent<PoolManager>();
        mWeaponPool.transform.name = "WeaponPool";
        mWeaponPool.transform.parent = transform;
        mWeaponPool.transform.localPosition = Vector3.zero;
        mWeaponPool.transform.localRotation = Quaternion.identity;
        mWeaponPool.Init(GameManager.instance.mWeaponJsonData[GameManager.instance.mWeaponCtrlData[mId].Id].PrefabId);
    }

    void Update()
    {
        if (!GameManager.instance.mIsLive)
            return;
        if (!mEnable)
            return;

        switch (GameManager.instance.mWeaponLastData[mId].WeaponType)
        {
            case Enum.WeaponType.Garlic:
                break;
            case Enum.WeaponType.Melee:
                transform.Rotate(Vector3.back * GameManager.instance.mWeaponLastData[mId].Speed * Time.deltaTime);
                break;
            case Enum.WeaponType.RangeBullet:
            case Enum.WeaponType.Web:
                mTime += Time.deltaTime;
                if (mTime > GameManager.instance.mWeaponLastData[mId].CoolTime)
                {
                    switch (GameManager.instance.mWeaponLastData[mId].WeaponType)
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
                        switch (GameManager.instance.mWeaponLastData[mId].WeaponType)
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
            melee = mWeaponPool.mPool[0].transform;

        melee.parent = mWeaponPool.transform;
        melee.localScale = Vector3.one * GameManager.instance.mWeaponLastData[mId].ProjectileSize;
        melee.localPosition = Vector3.zero;
        melee.localRotation = Quaternion.identity;
        melee.GetComponent<Melee>().Init(GameManager.instance.mWeaponLastData[mId].Damage);
    }
    public void PlacementCircle()
    {
        for (int i = 0; i < GameManager.instance.mWeaponLastData[mId].Projectile; ++i)
        {
            Transform melee;

            if (i < mWeaponPool.mPool.Count)
            {
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
                // dir = Quaternion.AngleAxis(rot, Vector3.forward) * dir;
                dir = Quaternion.Euler(0, 0, rot) * dir;
            }

            Transform bullet = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
            bullet.parent = mWeaponPool.transform;
            bullet.position = GameManager.instance.mPlayer.transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<Range>().Init(Mathf.RoundToInt(GameManager.instance.mWeaponLastData[mId].Damage), GameManager.instance.mWeaponLastData[mId].Pierce, dir, GameManager.instance.mWeaponLastData[mId].Speed); // -1 is Infinity Per
        }
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
                // dir = Quaternion.AngleAxis(rot, Vector3.forward) * dir;
                dir = Quaternion.Euler(0, 0, rot) * dir;
            }

            Transform bullet = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
            bullet.parent = mWeaponPool.transform;
            bullet.position = GameManager.instance.mPlayer.transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.localScale = new Vector3(0.02f, 0.4f, 1f);
            bullet.GetComponent<Range>().Init(Mathf.RoundToInt(GameManager.instance.mWeaponLastData[mId].Damage), GameManager.instance.mWeaponLastData[mId].Pierce, dir, GameManager.instance.mWeaponLastData[mId].Speed); // -1 is Infinity Per
        }
    }

    void FireDagger()
    {
        Transform bullet = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
        bullet.parent = mWeaponPool.transform;
        bullet.position = GameManager.instance.mPlayer.transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, GameManager.instance.mPlayer.mLastDir);
        bullet.GetComponent<Range>().Init(Mathf.RoundToInt(GameManager.instance.mWeaponLastData[mId].Damage), GameManager.instance.mWeaponLastData[mId].Pierce, GameManager.instance.mPlayer.mLastDir, GameManager.instance.mWeaponLastData[mId].Speed);
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
        // Debug.Log(dir);

        Transform bullet = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
        bullet.parent = mWeaponPool.transform;
        bullet.position = GameManager.instance.mPlayer.transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Range>().Init(Mathf.RoundToInt(GameManager.instance.mWeaponLastData[mId].Damage), GameManager.instance.mWeaponLastData[mId].Pierce, dir, GameManager.instance.mWeaponLastData[mId].Speed);
    }
}
