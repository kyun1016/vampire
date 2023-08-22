using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int mPrefabId;
    public float mDamage;
    public int mCount;
    public float mRange;
    public float mSpeed;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (mCount == 0)
            return;

        transform.Rotate(Vector3.back * mSpeed * Time.deltaTime);
        
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(mDamage + 10, mCount + 1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        mDamage = damage;
        mCount = count;

        Placement();
    }

    public void Init()
    {
        mSpeed = -150;
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
}
