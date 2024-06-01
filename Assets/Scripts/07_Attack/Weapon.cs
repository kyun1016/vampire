using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public eWeaponType mType;
    public int mId;
    public int mPrefabId;
    public float mDamage;
    public int mCount;
    public float mRange;
    public float mSpeed;

    public float mMaxTimer;
    public float mTimer;
    Player mPlayer;

    private void Awake()
    {
        mPlayer = GameManager.instance.mPlayer;
    }

    public void Init(int id)
    {
        mId = id;
        ItemData data = GameManager.instance.mItemData[mId];
        // Basic Set
        name = "Weapon " + data.Id;
        transform.parent = GameManager.instance.mPlayer.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (data.ItemType == eItemType.Melee)
            mType = eWeaponType.Melee;
        else if (data.ItemType == eItemType.Range)
            mType = eWeaponType.Range;
        else
            Debug.Assert(false, "Error");
        mDamage = data.Damages[0];
        mCount = data.Counts[0];

        for(int i=0;i< GameManager.instance.mPoolManager.mPrefabs.Length; ++i)
        {
            if(data.Projectile == GameManager.instance.mPoolManager.mPrefabs[i])
            {
                mPrefabId = i;
                break;
            }
        }
        switch (mType)
        {
            case eWeaponType.Melee:
                mSpeed = 150;
                mRange = 1.5f;
                Placement();
                break;
            case eWeaponType.Range:
                mSpeed = 10;
                mMaxTimer = 0.4f;
                break;
            default:
                Debug.Assert(false, "Error");
                break;
        }

        mPlayer.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Update()
    {
        if (!GameManager.instance.mIsLive)
            return;

        switch (mType)
        {
            case eWeaponType.Melee:
                transform.Rotate(Vector3.back * mSpeed * Time.deltaTime);
                break;
            case eWeaponType.Range:
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
        mDamage = GameManager.instance.mItemData[mId].Damages[GameManager.instance.mItemLevel[mId]];
        mCount = GameManager.instance.mItemData[mId].Counts[GameManager.instance.mItemLevel[mId]];

        mPlayer.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);

        if(mType == eWeaponType.Melee)
            Placement();
    }
    void Placement()
    {
        for (int i = 0; i < mCount; ++i)
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

            Vector3 rotVec = Vector3.forward * 360 * i / mCount;
            melee.Rotate(rotVec);
            melee.Translate(melee.up * mRange, Space.World);
            melee.GetComponent<Melee>().Init(mDamage);
        }
    }

    void Fire()
    {
        if (!mPlayer.mScanner.mNearestTarget)
            return;

        Vector3 targetPos = mPlayer.mScanner.mNearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.mPoolManager.Get(mPrefabId).transform; // 부족한 것을 오브젝트 풀로 추가
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Range>().Init(mDamage, mCount, dir, mSpeed); // -1 is Infinity Per
        bullet.parent = transform;
    }
}
