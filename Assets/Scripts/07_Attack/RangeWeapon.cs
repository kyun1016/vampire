using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    public int mPrefabId;
    public float mDamage;
    public int mPer;
    public float mSpeed;
    public float mBulletSpeed;

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
        mTimer += Time.deltaTime;
        if (mTimer > mSpeed)
        {
            mTimer = 0f;
            Fire();
        }

        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(mDamage + 10, mPer + 1);
        }
    }

    public void LevelUp(float damage, int per)
    {
        mDamage = damage;
        mPer = per;
    }

    public void Init()
    {
        mSpeed = 0.3f;
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
        bullet.GetComponent<Bullet>().Init(mDamage, mPer, dir, mBulletSpeed); // -1 is Infinity Per
        bullet.parent = transform;
    }
}
