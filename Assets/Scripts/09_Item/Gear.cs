using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType mType;
    public float mRate;

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.mPlayer.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // Property Set
        mType = data.itemType;
        mRate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        mRate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch(mType)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }

    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons)
        {
            switch (weapon.mId)
            {
                case 0:
                    weapon.mSpeed = 150 + (150 * mRate);
                    break;
                case 1:
                    weapon.mSpeed = 10 + (10 * mRate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.mPlayer.mSpeed = speed + speed * mRate;
    }
}
