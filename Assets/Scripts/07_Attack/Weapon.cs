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

    public float mTimer;
    Player mPlayer;

    private void Awake()
    {
        mPlayer = GetComponentInParent<Player>();
    }
    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (mId)
        {
            case 0:
                transform.Rotate(Vector3.back * mSpeed * Time.deltaTime);
                break;
            default:
                mTimer += Time.deltaTime;

                if(mTimer > mSpeed)
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

        if(mId == 0)
        {
            Placement();
        }
    }

    public void Init()
    {
        switch (mId)
        {
            case 0:
                mSpeed = -150;
                Placement();
                break;
            default:
                mSpeed = 0.3f;
                break;
        }
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
            bullet.GetComponent<Bullet>().Init(mDamage, -1, Vector3.zero); // -1 is Infinity Per
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
        bullet.GetComponent<Bullet>().Init(mDamage, mCount, dir); // -1 is Infinity Per
    }
}
