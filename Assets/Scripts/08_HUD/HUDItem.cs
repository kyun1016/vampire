using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDItem : MonoBehaviour
{
    public int mId;

    Image mIcon;
    TMP_Text mTextLevel;
    TMP_Text mTextName;
    TMP_Text mTextDesc;

    private void Awake()
    {
        mIcon = GetComponentsInChildren<Image>()[1];

        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        mTextLevel = texts[0];
        mTextName = texts[1];
        mTextDesc = texts[2];

        mIcon.sprite = GameManager.instance.mItemSprite[GameManager.instance.mItemData[mId].Id];
        mTextName.text = GameManager.instance.mItemData[mId].Name;
    }

    private void OnEnable()
    {
        mTextLevel.text = "Lv." + (GameManager.instance.mItemLevel[mId] + 1);
        switch (GameManager.instance.mItemData[mId].ItemType)
        {
            case Enum.ItemType.Melee:
                mTextDesc.text = string.Format(GameManager.instance.mItemData[mId].Desc, GameManager.instance.mItemData[mId].Damage[GameManager.instance.mItemLevel[mId]] * 100, GameManager.instance.mItemData[mId].Projectile[GameManager.instance.mItemLevel[mId]], GameManager.instance.mItemData[mId].Info1[GameManager.instance.mItemLevel[mId]]);
                break;
            case Enum.ItemType.Range:
                mTextDesc.text = string.Format(GameManager.instance.mItemData[mId].Desc, GameManager.instance.mItemData[mId].Damage[GameManager.instance.mItemLevel[mId]] * 100, GameManager.instance.mItemData[mId].Projectile[GameManager.instance.mItemLevel[mId]], GameManager.instance.mItemData[mId].Info1[GameManager.instance.mItemLevel[mId]], GameManager.instance.mItemData[mId].Pierce[GameManager.instance.mItemLevel[mId]]);
                break;
            case Enum.ItemType.Glove:
            case Enum.ItemType.Shoe:
                mTextDesc.text = string.Format(GameManager.instance.mItemData[mId].Desc, GameManager.instance.mItemData[mId].Damage[GameManager.instance.mItemLevel[mId]] * 100);
                break;
            case Enum.ItemType.Heal:
                mTextDesc.text = string.Format(GameManager.instance.mItemData[mId].Desc);
                break;
            default:
                Debug.Assert(false, "Error");
                break;
        }
    }

    private void LateUpdate()
    {
        mTextLevel.text = "Lv." + (GameManager.instance.mItemLevel[mId] + 1);
    }

    public void OnClick()
    {
        switch (GameManager.instance.mItemData[mId].ItemType)
        {
            case Enum.ItemType.Melee:
            case Enum.ItemType.Range:
                if (GameManager.instance.mItemLevel[mId] == 0)
                {
                    GameObject newWeapon = new GameObject();
                    GameManager.instance.mPlayer.mWeapons[mId] = newWeapon.AddComponent<Weapon>();

                    GameManager.instance.mPlayer.mWeapons[mId].Init(mId);
                }
                else
                {
                    GameManager.instance.mPlayer.mWeapons[mId].LevelUp();
                }
                ++GameManager.instance.mItemLevel[mId];
                break;
            case Enum.ItemType.Glove:
            case Enum.ItemType.Shoe:
                ++GameManager.instance.mItemLevel[mId];
                if (GameManager.instance.mItemLevel[mId] == 1)
                {
                    GameObject newGear = new GameObject();
                    GameManager.instance.mPlayer.mGears[mId] = newGear.AddComponent<Gear>();

                    GameManager.instance.mPlayer.mGears[mId].Init(mId);
                }
                else
                {
                    GameManager.instance.mPlayer.mGears[mId].LevelUp();
                }
                break;
            case Enum.ItemType.Heal:
                GameManager.instance.mHealth = GameManager.instance.mMaxHealth;
                break;
            default:
                Debug.Assert(false, "Error");
                break;
        }

        if(GameManager.instance.mItemLevel[mId] == GameManager.instance.mItemData[mId].Damage.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
