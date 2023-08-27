using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData mData;
    public int mLevel;
    public MeleeWeapon mMeleeWeapon;
    public RangeWeapon mRangeWeapon;

    Image mIcon;
    TMP_Text mTextLevel;

    private void Awake()
    {
        mIcon = GetComponentsInChildren<Image>()[1];
        mIcon.sprite = mData.itemIcon;

        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

        mTextLevel = texts[0];
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
                break;
            case ItemData.ItemType.Range:
                break;
            case ItemData.ItemType.Glove:
                break;
            case ItemData.ItemType.Shoe:
                break;
            case ItemData.ItemType.Heal:
                break;
        }

        mLevel++;

        if(mLevel == mData.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
