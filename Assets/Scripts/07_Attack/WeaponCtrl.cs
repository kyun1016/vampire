using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : MonoBehaviour
{
    int mId;
    float mTime;
    PoolManager mWeaponPool;

    public void Init(int id)
    {
        if (id >= GameManager.instance.mWeaponSize)
            Debug.Assert(false, "Error");
        mId = id;

        mWeaponPool = new GameObject().AddComponent<PoolManager>();
        mWeaponPool.transform.name = "WeaponPool";
        mWeaponPool.transform.parent = GameManager.instance.mPlayer.transform;
        mWeaponPool.Init(GameManager.instance.mWeaponJsonData[GameManager.instance.mWeaponCtrlData[mId].Id].SpriteId);
    }

    void Update()
    {
        if (!GameManager.instance.mIsLive)
            return;

        switch (GameManager.instance.mWeaponLastData[mId].WeaponType)
        {
            case Enum.WeaponType.Melee:
                transform.Rotate(Vector3.back * GameManager.instance.mWeaponLastData[mId].Speed * Time.deltaTime);
                break;
            case Enum.WeaponType.Range:
                mTime += Time.deltaTime;
                if (mTime > GameManager.instance.mWeaponLastData[mId].CoolTime)
                {
                    mTime = 0f;
                    Fire();
                }
                break;
            default:
                Debug.Assert(false, "Error");
                break;
        }
    }

    public void Placement()
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
        Vector3 dir = targetPos - GameManager.instance.mPlayer.transform.position;
        dir = dir.normalized;

        Transform bullet = mWeaponPool.Get().transform; // 부족한 것을 오브젝트 풀로 추가
        bullet.parent = mWeaponPool.transform;

        bullet.position = GameManager.instance.mPlayer.transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Range>().Init(Mathf.RoundToInt(GameManager.instance.mWeaponLastData[mId].Damage), GameManager.instance.mWeaponLastData[mId].Pierce, dir, GameManager.instance.mWeaponLastData[mId].Speed); // -1 is Infinity Per
    }
}
