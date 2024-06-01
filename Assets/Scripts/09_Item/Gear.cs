using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public int mId;

    public void Init(int id)
    {
        mId = id;
        // Basic Set
        name = "Gear " + id;
        transform.parent = GameManager.instance.mPlayer.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // Property Set
        ApplyGear();
    }

    public void LevelUp()
    {
        ApplyGear();
    }

    void ApplyGear()
    {
        switch(GameManager.instance.mItemData[mId].ItemType)
        {
            case eItemType.Glove:
                RateUp();
                break;
            case eItemType.Shoe:
                SpeedUp();
                break;
        }

    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons)
        {
            switch (GameManager.instance.mItemData[mId].ItemType)
            {
                case eItemType.Melee:
                    weapon.mSpeed = 150 + (150 * GameManager.instance.mItemData[mId].Damages[GameManager.instance.mItemLevel[mId]]);
                    break;
                case eItemType.Range:
                    weapon.mSpeed = 10 + (10 * GameManager.instance.mItemData[mId].Damages[GameManager.instance.mItemLevel[mId]]);
                    break;
                default:
                    Debug.Assert(false, "Error");
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.mPlayer.mSpeed = speed + speed * GameManager.instance.mItemData[mId].Damages[GameManager.instance.mItemLevel[mId]];
    }
}
