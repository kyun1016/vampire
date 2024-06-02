using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int mId;
    public int mPrefabId;
    public float mDamage;
    public int mProjectile;
    public int mPierce;
    public float mRange;
    public float mSpeed;

    public float mMaxTimer;
    public float mTimer;

    public void Init(int id)
    {
        mId = id;
        // Basic Set
        name = "WeaponPool" + mId;
        transform.parent = GameManager.instance.mPlayer.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        for(int i=0;i< GameManager.instance.mPrefabs.Length; ++i)
        {
            if(GameManager.instance.mItemProjectile[mId] == GameManager.instance.mPrefabs[i])
            {
                mPrefabId = i;
                break;
            }
        }

        mDamage = GameManager.instance.mItemData[mId].Damage[0];
        mSpeed = GameManager.instance.mItemData[mId].Speed[0];
        mProjectile = GameManager.instance.mItemData[mId].Projectile[0];
        if (GameManager.instance.mItemData[mId].ItemType == Enum.ItemType.Range)
        {
            mPierce = GameManager.instance.mItemData[mId].Pierce[0];
            mMaxTimer = GameManager.instance.mItemData[mId].Info1[0];
        }
        else
        {
            mRange = GameManager.instance.mItemData[mId].Info1[0];
            Placement();
        }

        GameManager.instance.mPlayer.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Update()
    {
        if (!GameManager.instance.mIsLive)
            return;

        switch (GameManager.instance.mItemData[mId].ItemType)
        {
            case Enum.ItemType.Melee:
                transform.Rotate(Vector3.back * mSpeed * Time.deltaTime);
                break;
            case Enum.ItemType.Range:
                mTimer += Time.deltaTime;
                if (mTimer > mMaxTimer)
                {
                    mTimer = 0f;
                    Fire();
                }
                break;
            default:
                Debug.Assert(false, "Error");
                break;
        }
    }
    public void LevelUp()
    {
        mDamage = GameManager.instance.mItemData[mId].Damage[GameManager.instance.mItemLevel[mId]];
        mProjectile = GameManager.instance.mItemData[mId].Projectile[GameManager.instance.mItemLevel[mId]];

        GameManager.instance.mPlayer.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);

        if(GameManager.instance.mItemData[mId].ItemType == Enum.ItemType.Melee)
            Placement();
    }
    public void Placement()
    {
        for (int i = 0; i < mProjectile; ++i)
        {
            Transform melee;

            if (i < transform.childCount)
            {
                melee = transform.GetChild(i);
            }
            else
            {
                melee = GameManager.instance.mPoolManager.Get(mPrefabId).transform; // 부족한 것을 오브젝트 풀로 추가
                melee.parent = transform;
            }

            melee.localPosition = Vector3.zero;
            melee.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / mProjectile;
            melee.Rotate(rotVec);
            melee.Translate(melee.up * mRange, Space.World);
            melee.GetComponent<Melee>().Init(mDamage);
        }
    }

    void Fire()
    {
        if (!GameManager.instance.mPlayer.mScanner.mNearestTarget)
            return;

        Vector3 targetPos = GameManager.instance.mPlayer.mScanner.mNearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.mPoolManager.Get(mPrefabId).transform; // 부족한 것을 오브젝트 풀로 추가
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Range>().Init(mDamage, mPierce, dir, mSpeed); // -1 is Infinity Per
        bullet.parent = transform;
    }
}
