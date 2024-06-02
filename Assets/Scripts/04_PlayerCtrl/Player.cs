using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 mInputVec;
    public float mSpeed;
    public EnemyScanner mScanner;

    Animator mAnim;
    SpriteRenderer mSpriter;
    Rigidbody2D mRigid;

    public int mWeaponCount = 0;
    public int mGearCount = 0;
    public Weapon[] mWeapons;
    public Gear[] mGears;

    private void Awake()
    {
        mWeapons = new Weapon[8];
        mGears = new Gear[8];
    }
    void Start()
    {
        mAnim = GetComponent<Animator>();
        mSpriter = GetComponent<SpriteRenderer>();
        mRigid = GetComponent<Rigidbody2D>();
        mScanner = GetComponent<EnemyScanner>();
    }
    void OnMove(InputValue value)
    {
        if (!GameManager.instance.mIsLive)
            return;
        mInputVec = value.Get<Vector2>();
    }
    void FixedUpdate()
    {
        if (!GameManager.instance.mIsLive)
            return;
        Vector2 nextVec = mInputVec * mSpeed * Time.fixedDeltaTime;
        mRigid.MovePosition(mRigid.position + nextVec);
    }
    void LateUpdate()
    {
        if (!GameManager.instance.mIsLive)
            return;
        mAnim.SetFloat("Speed", mInputVec.magnitude);
        if(mInputVec.x != 0)
        {
            mSpriter.flipX = mInputVec.x < 0;
        }
    }
}