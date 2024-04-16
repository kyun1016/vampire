using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
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

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = mPlayer.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        mId = data.itemId;
        mDamage = data.baseDamage;
        mCount = data.baseCount;

        for(int i=0;i< GameManager.instance.mPoolManager.mPrefabs.Length; ++i)
        {
            if(data.projectile == GameManager.instance.mPoolManager.mPrefabs[i])
            {
                mPrefabId = i;
                break;
            }
        }
        switch (mId)
        {
            case 0:
                mSpeed = 150;
                mRange = 1.5f;
                Placement();
                break;
            case 1:
                mSpeed = 10;
                mMaxTimer = 0.4f;
                break;
        }

        mPlayer.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Update()
    {
        if (!GameManager.instance.mIsLive)
            return;

        switch (mId)
        {
            case 0:
                transform.Rotate(Vector3.back * mSpeed * Time.deltaTime);
                break;
            default:
                mTimer += Time.deltaTime;
                if (mTimer > mMaxTimer)
                {
                    mTimer = 0f;
                    Fire();
                }
                break;
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(mDamage + 10, mCount + 1);
        }
    }
    public void LevelUp(float damage, int count)
    {
        mDamage = damage;
        mCount = count;

        mPlayer.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);

        Placement();
    }
    void Placement()
    {
        for (int i = 0; i < mCount; ++i)
        {
            Transform bullet;

            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.mPoolManager.Get(mPrefabId).transform; // 부족한 것을 오브젝트 풀로 추가
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / mCount;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * mRange, Space.World);
            bullet.GetComponent<Melee>().Init(mDamage);
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
        bullet.GetComponent<Bullet>().Init(mDamage, mCount, dir, mSpeed); // -1 is Infinity Per
        bullet.parent = transform;
    }
}
