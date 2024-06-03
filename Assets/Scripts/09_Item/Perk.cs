//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Perk : MonoBehaviour
//{
//    public int mId;

//    public void Init(int id)
//    {
//        mId = id;

//        // Property Set
//        ApplyGear();
//    }

//    public void LevelUp()
//    {
//        ApplyGear();
//    }

//    void ApplyGear()
//    {
//        switch(GameManager.instance.mItemData[mId].ItemType)
//        {
//            case Enum.ItemType.Glove:
//                RateUp();
//                break;
//            case Enum.ItemType.Shoe:
//                SpeedUp();
//                break;
//        }

//    }

//    void RateUp()
//    {
//        // Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

//        foreach(Weapon weapon in GameManager.instance.mWeapons)
//        {
//            if (weapon == null)
//                continue;
//            switch (GameManager.instance.mItemData[weapon.mId].ItemType)
//            {
//                case Enum.ItemType.Melee:
//                    weapon.mSpeed += weapon.mSpeed * GameManager.instance.mItemData[mId].Damage[GameManager.instance.mItemLevel[mId] - 1];
//                    weapon.Placement();
//                    break;
//                case Enum.ItemType.Range:
//                    weapon.mSpeed += weapon.mSpeed * GameManager.instance.mItemData[mId].Damage[GameManager.instance.mItemLevel[mId] - 1];
//                    break;
//                default:
//                    Debug.Assert(false, "Error");
//                    break;
//            }
//        }
//    }

//    void SpeedUp()
//    {
//        float speed = 3;
//        GameManager.instance.mPlayer.mSpeed = speed + speed * GameManager.instance.mItemData[mId].Damage[GameManager.instance.mItemLevel[mId] - 1];
//    }
//}
