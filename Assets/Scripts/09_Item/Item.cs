using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData mData;
    public int mLevel;
    public Weapon mWeapon;
    public Gear mGear;

    Image mIcon;
    TMP_Text mTextLevel;
    TMP_Text mTextName;
    TMP_Text mTextDesc;

    private void Awake()
    {
        mIcon = GetComponentsInChildren<Image>()[1];
        mIcon.sprite = mData.itemIcon;

        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        mTextLevel = texts[0];
        mTextName = texts[1];
        mTextDesc = texts[2];

        mTextName.text = mData.itemName;
    }

    private void OnEnable()
    {
        mTextLevel.text = "Lv." + (mLevel + 1);
        switch (mData.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                mTextDesc.text = string.Format(mData.itemDesc, mData.damages[mLevel] * 100, mData.counts[mLevel]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                mTextDesc.text = string.Format(mData.itemDesc, mData.damages[mLevel] * 100);
                break;
            default:
                mTextDesc.text = string.Format(mData.itemDesc);
                break;

        }
        
    }

    private void LateUpdate()
    {
        mTextLevel.text = "Lv." + (mLevel + 1);
    }

    public void OnClick()
    {
        switch (mData.itemType)
        {
            case ItemData.ItemType.Melee:
                if (mLevel == 0)
                {
                    GameObject newWeapon = new GameObject();
                    mWeapon = newWeapon.AddComponent<Weapon>();
                    mWeapon.Init(mData);
                }
                else
                {
                    float nextDamage = mData.baseDamage;
                    int nextCount = 0;

                    nextDamage += mData.baseDamage * mData.damages[mLevel];
                    nextCount += mData.counts[mLevel];

                    mWeapon.LevelUp(nextDamage, nextCount, true);
                }
                mLevel++;
                break;
            case ItemData.ItemType.Range:
                if (mLevel == 0)
                {
                    GameObject newWeapon = new GameObject();
                    mWeapon = newWeapon.AddComponent<Weapon>();
                    mWeapon.Init(mData);
                }
                else
                {
                    float nextDamage = mData.baseDamage;
                    int nextCount = 0;

                    nextDamage += mData.baseDamage * mData.damages[mLevel];
                    nextCount += mData.counts[mLevel];

                    mWeapon.LevelUp(nextDamage, nextCount);
                }
                mLevel++;
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (mLevel == 0)
                {
                    GameObject newGear = new GameObject();
                    mGear = newGear.AddComponent<Gear>();
                    mGear.Init(mData);
                }
                else
                {
                    float nextRate = mData.damages[mLevel];
                    mGear.LevelUp(nextRate);
                }
                mLevel++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.mHealth = GameManager.instance.mMaxHealth;
                break;
        }

        if(mLevel == mData.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
