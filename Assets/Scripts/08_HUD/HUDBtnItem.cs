using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDBtnItem : MonoBehaviour
{
    public int mId;

    Image mIcon;
    TMP_Text mTextLevel;
    TMP_Text mTextName;
    TMP_Text mTextDesc;

    public void UpdateTextName()
    {
        if (mId < GameManager.instance.mWeaponJsonData.Length)
        {
            mTextName.text = GameManager.instance.mTextJsonData[(int)GameManager.instance.mSettingData.LanguageType].WeaponName[mId];
        }
        else
        {
            int idx = mId - GameManager.instance.mWeaponJsonData.Length;
            mTextName.text = GameManager.instance.mTextJsonData[(int)GameManager.instance.mSettingData.LanguageType].PerkName[idx];
        }
    }
    public void UpdateTextDesc()
    {
        int level = 0;

        if (mId < GameManager.instance.mWeaponJsonData.Length)
        {
            for (int i = 0; i < GameManager.instance.mPlayerData.WeaponSize; ++i)
            {
                if (mId == GameManager.instance.mWeaponCtrlData[i].Id)
                    level = GameManager.instance.mWeaponCtrlData[i].Level;
            }
            if(level == 0)
            {
                mTextDesc.text = GameManager.instance.mTextJsonData[(int)GameManager.instance.mSettingData.LanguageType].WeaponInitDesc[mId];
            }
            else
            {
                switch (GameManager.instance.mWeaponJsonData[mId].DescType)
                {
                    case Enum.DescType.Melee:
                        mTextDesc.text = string.Format(GameManager.instance.mTextJsonData[(int)GameManager.instance.mSettingData.LanguageType].WeaponLvUpDesc[0],
                            GameManager.instance.mWeaponJsonData[mId].Damage[level] * 100, GameManager.instance.mWeaponJsonData[mId].Projectile[level], GameManager.instance.mWeaponJsonData[mId].Speed[level]);
                        break;
                    case Enum.DescType.Range:
                        mTextDesc.text = string.Format(GameManager.instance.mTextJsonData[(int)GameManager.instance.mSettingData.LanguageType].WeaponLvUpDesc[1],
                            GameManager.instance.mWeaponJsonData[mId].Damage[level] * 100, GameManager.instance.mWeaponJsonData[mId].Projectile[level], GameManager.instance.mWeaponJsonData[mId].CoolTime[level], GameManager.instance.mWeaponJsonData[mId].Pierce[level]);
                        break;
                    default:
                        Debug.Assert(false, "Error");
                        break;
                }
            }

        }
        else
        {
            int idx = mId - GameManager.instance.mWeaponJsonData.Length;
            for (int i = 0; i < GameManager.instance.mPlayerData.PerkSize; ++i)
            {
                if (idx == GameManager.instance.mPerkCtrlData[i].Id)
                    level = GameManager.instance.mPerkCtrlData[i].Level;
            }

            switch (GameManager.instance.mPerkJsonData[idx].DescType)
            {
                case Enum.DescType.Perk:
                    mTextDesc.text = string.Format(GameManager.instance.mTextJsonData[(int)GameManager.instance.mSettingData.LanguageType].PerkDesc[idx], GameManager.instance.mPerkJsonData[idx].Damage[level] * 100);
                    break;
                case Enum.DescType.Heal:
                    mTextDesc.text = GameManager.instance.mTextJsonData[(int)GameManager.instance.mSettingData.LanguageType].PerkDesc[idx];
                    break;
                default:
                    Debug.Assert(false, "Error");
                    break;
            }
        }

        mTextLevel.text = "Lv." + (level + 1);
    }
    public void UpdateText()
    {
        UpdateTextName();
        UpdateTextDesc();
    }

    void UpdateSprite()
    {
        if (mId < GameManager.instance.mWeaponJsonData.Length)
        {
            mIcon.sprite = GameManager.instance.mHUDBtnItemSprite[GameManager.instance.mWeaponJsonData[mId].HUDBtnItemSpriteId];
        }
        else
        {
            int idx = mId - GameManager.instance.mWeaponJsonData.Length;
            mIcon.sprite = GameManager.instance.mHUDBtnItemSprite[GameManager.instance.mPerkJsonData[idx].HUDBtnItemSpriteId];
        }
    }
    private void Awake()
    {
        mIcon = GetComponentsInChildren<Image>()[1];

        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        mTextLevel = texts[0];
        mTextName = texts[1];
        mTextDesc = texts[2];

        UpdateText();
        UpdateSprite();
    }

    private void OnEnable()
    {
        UpdateText();
        UpdateSprite();
    }

    private void LateUpdate()
    {
        // mTextLevel.text = "Lv." + (GameManager.instance.mItemLevel[mId] + 1);
    }

    public void OnClick()
    {
        int idx = mId;
        int idxCtrl = -1;
        int level = 0;
        if (mId < GameManager.instance.mWeaponJsonData.Length)
        {
            for (int i = 0; i < GameManager.instance.mPlayerData.WeaponSize; ++i)
            {
                if (idx == GameManager.instance.mWeaponCtrlData[i].Id)
                {
                    idxCtrl = i;
                    level = GameManager.instance.mWeaponCtrlData[i].Level;
                }
            }
            switch (GameManager.instance.mWeaponJsonData[mId].DescType)
            {
                case Enum.DescType.Melee:
                case Enum.DescType.Range:
                    if (level == 0)
                    {
                        idxCtrl = GameManager.instance.mPlayerData.WeaponSize++;
                        GameManager.instance.mWeaponCtrlData[idxCtrl].Id = idx;
                        GameManager.instance.mWeaponCtrlData[idxCtrl].Level = 0;
                        GameManager.instance.mPlayer.mWeaponCtrl[idxCtrl].Init(idxCtrl);
                    }

                    FuncWeapon.InitWeaponIdx(idxCtrl);
                    FuncWeapon.UpdateWeaponLastIdx(idxCtrl);
                    ++GameManager.instance.mWeaponCtrlData[idxCtrl].Level;
                    break;
                default:
                    Debug.Assert(false, "Error");
                    break;
            }
            if (GameManager.instance.mWeaponCtrlData[idxCtrl].Level == GameManager.instance.mWeaponJsonData[idx].Damage.Length)
            {
                GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            idx -= GameManager.instance.mWeaponJsonData.Length;
            for (int i = 0; i < GameManager.instance.mPlayerData.PerkSize; ++i)
            {
                if (idx == GameManager.instance.mPerkCtrlData[i].Id)
                {
                    idxCtrl = i;
                    level = GameManager.instance.mPerkCtrlData[i].Level;
                }

            }
            switch (GameManager.instance.mPerkJsonData[idx].DescType)
            {
                case Enum.DescType.Perk:
                    if (level == 0)
                    {
                        idxCtrl = GameManager.instance.mPlayerData.PerkSize++;
                        GameManager.instance.mPerkCtrlData[idxCtrl].Id = idx;
                        GameManager.instance.mPerkCtrlData[idxCtrl].Level = 0;
                    }
                    FuncWeapon.UpdatePerkIdx(idxCtrl);
                    FuncWeapon.UpdatePlayerMovement();
                    FuncWeapon.ReloadWeaponLast();
                    ++GameManager.instance.mPerkCtrlData[idxCtrl].Level;
                    break;
                case Enum.DescType.Heal:
                    GameManager.instance.mPlayerData.Health = GameManager.instance.mPlayerData.MaxHealth;
                    break;
                default:
                    Debug.Assert(false, "Error");
                    break;

            }
            if ((idxCtrl != -1) && (GameManager.instance.mPerkCtrlData[idxCtrl].Level == GameManager.instance.mPerkJsonData[idx].Damage.Length))
            {
                GetComponent<Button>().interactable = false;
            }
        }
        for (int i = 0; i < GameManager.instance.mPlayerData.WeaponSize; ++i)
        {
            GameManager.instance.mPlayer.mWeaponCtrl[i].updatePool();       
        }
    }
}
