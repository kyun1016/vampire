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

    Weapon mWeapon;
    Gear mGear;

    private void Awake()
    {
        mIcon = GetComponentsInChildren<Image>()[1];

        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        mTextLevel = texts[0];
        mTextName = texts[1];
        mTextDesc = texts[2];

        Debug.Log(GameManager.instance.mItemData[mId].Id);
        mIcon.sprite = GameManager.instance.mItemSprite[GameManager.instance.mItemData[mId].Id];
        mTextName.text = GameManager.instance.mItemData[mId].Name;
    }

    private void OnEnable()
    {
        mTextLevel.text = "Lv." + (GameManager.instance.mItemLevel[mId] + 1);
        switch (GameManager.instance.mItemData[mId].ItemType)
        {
            case eItemType.Melee:
            case eItemType.Range:
                mTextDesc.text = string.Format(GameManager.instance.mItemData[mId].Desc, GameManager.instance.mItemData[mId].Damages[GameManager.instance.mItemLevel[mId]] * 100, GameManager.instance.mItemData[mId].Counts[GameManager.instance.mItemLevel[mId]]);
                break;
            case eItemType.Glove:
            case eItemType.Shoe:
                mTextDesc.text = string.Format(GameManager.instance.mItemData[mId].Desc, GameManager.instance.mItemData[mId].Damages[GameManager.instance.mItemLevel[mId]] * 100);
                break;
            case eItemType.Heal:
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
            case eItemType.Melee:
            case eItemType.Range:
                if (GameManager.instance.mItemLevel[mId] == 0)
                {
                    GameObject newWeapon = new GameObject();
                    mWeapon = newWeapon.AddComponent<Weapon>();
                    mWeapon.Init(mId);
                }
                else
                {
                    mWeapon.LevelUp();
                }
                ++GameManager.instance.mItemLevel[mId];
                break;
            case eItemType.Glove:
            case eItemType.Shoe:
                if (GameManager.instance.mItemLevel[mId] == 0)
                {
                    GameObject newGear = new GameObject();
                    mGear = newGear.AddComponent<Gear>();
                    mGear.Init(mId);
                }
                else
                {
                    mGear.LevelUp();
                }
                ++GameManager.instance.mItemLevel[mId];
                break;
            case eItemType.Heal:
                GameManager.instance.mHealth = GameManager.instance.mMaxHealth;
                break;
            default:
                Debug.Assert(false, "Error");
                break;
        }

        if(GameManager.instance.mItemLevel[mId] == GameManager.instance.mItemData[mId].Damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
