using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public Enum.DropItemSprite mType;
    public int mData;

    public bool mEnable;
    Rigidbody2D mRigid;
    Rigidbody2D mTarget;

    private void Awake()
    {
        mRigid = GetComponent<Rigidbody2D>();
        mTarget = GameManager.instance.mPlayer.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        mEnable = false;
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.mIsLive)
            return;
        if (!mEnable)
            return;

        Vector2 dirVec = mTarget.position - mRigid.position;
        Vector2 nextVec = dirVec.normalized * 10 * Time.fixedDeltaTime;
        mRigid.MovePosition(mRigid.position + nextVec);
        mRigid.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mEnable = false;
            switch (mType)
            {
                case Enum.DropItemSprite.Exp0:
                case Enum.DropItemSprite.Exp1:
                case Enum.DropItemSprite.Exp2:
                    GameManager.instance.GetExp(mData);
                    break;
                case Enum.DropItemSprite.Health:
                    GameManager.instance.GetHealth(mData);
                    break;
                case Enum.DropItemSprite.Mag:
                    foreach (GameObject item in GameManager.instance.mDropPool.mPool)
                    {
                        if (item.GetComponent<DropItem>().mType == Enum.DropItemSprite.Exp0 || item.GetComponent<DropItem>().mType == Enum.DropItemSprite.Exp1 || item.GetComponent<DropItem>().mType == Enum.DropItemSprite.Exp2)
                            item.GetComponent<DropItem>().mEnable = true;
                    }
                    break;
                case Enum.DropItemSprite.Gold0:
                case Enum.DropItemSprite.Gold1:
                case Enum.DropItemSprite.Gold2:
                    GameManager.instance.GetGold(mData);
                    break;
                default:
                    Debug.Assert(false, "Error");
                    break;
            }
            gameObject.SetActive(false);
            return;
        }

        if (collision.CompareTag("PickupCollider"))
            mEnable = true;
    }
}
