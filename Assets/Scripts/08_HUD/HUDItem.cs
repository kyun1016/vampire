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

        mIcon.sprite = GameManager.instance.mItemSprite[GameManager.instance.mWeaponJsonData[mId].SpriteId];
        mTextName.text = GameManager.instance.mWeaponJsonData[mId].Name;
    }

    private void OnEnable()
    {
        int idx = mId;
        int level = 0;
        if (mId < GameManager.instance.mWeaponJsonData.Length)
        {
            for (int i = 0; i < GameManager.instance.mWeaponSize; ++i)
            {
                if (idx == GameManager.instance.mWeaponCtrlData[i].Id)
                    level = GameManager.instance.mWeaponCtrlData[i].Level;
            }
            switch (GameManager.instance.mWeaponJsonData[mId].WeaponType)
            {
                case Enum.WeaponType.Melee:
                    mTextDesc.text = string.Format(GameManager.instance.mWeaponJsonData[idx].Desc, GameManager.instance.mWeaponJsonData[idx].Damage[level] * 100, GameManager.instance.mWeaponJsonData[idx].Projectile[level], GameManager.instance.mWeaponJsonData[idx].Speed[level]);
                    break;
                case Enum.WeaponType.Range:
                    mTextDesc.text = string.Format(GameManager.instance.mWeaponJsonData[idx].Desc, GameManager.instance.mWeaponJsonData[idx].Damage[level] * 100, GameManager.instance.mWeaponJsonData[idx].Projectile[level], GameManager.instance.mWeaponJsonData[idx].CoolTime[level], GameManager.instance.mWeaponJsonData[idx].Pierce[level]);
                    break;
                default:
                    Debug.Assert(false, "Error");
                    break;
            }
        }
        else
        {
            idx -= GameManager.instance.mWeaponJsonData.Length;
            for (int i = 0; i < GameManager.instance.mPerkSize; ++i)
            {
                if (idx == GameManager.instance.mPerkCtrlData[i].Id)
                    level = GameManager.instance.mPerkCtrlData[i].Level;
            }

            switch (GameManager.instance.mPerkJsonData[mId].DescType)
            {
                case Enum.DescType.Perk:
                    mTextDesc.text = string.Format(GameManager.instance.mPerkJsonData[idx].Desc, GameManager.instance.mPerkJsonData[idx].Damage[level] * 100);
                    break;
                case Enum.DescType.Heal:
                    mTextDesc.text = string.Format(GameManager.instance.mPerkJsonData[mId].Desc);
                    break;
                default:
                    Debug.Assert(false, "Error");
                    break;
            }
        }
        mTextLevel.text = "Lv." + (level + 1);
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
            for (int i = 0; i < GameManager.instance.mWeaponSize; ++i)
            {
                if (idx == GameManager.instance.mWeaponCtrlData[i].Id)
                {
                    idxCtrl = i;
                    level = GameManager.instance.mWeaponCtrlData[i].Level;
                }
            }
            switch (GameManager.instance.mWeaponJsonData[mId].WeaponType)
            {
                case Enum.WeaponType.Melee:
                case Enum.WeaponType.Range:
                    if (level == 0)
                    {
                        idxCtrl = GameManager.instance.mWeaponSize++;
                        GameManager.instance.mWeaponCtrlData[idxCtrl].Id = idx;
                        GameManager.instance.mWeaponCtrlData[idxCtrl].Level = 0;
                    }

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
            for (int i = 0; i < GameManager.instance.mPerkSize; ++i)
            {
                if (idx == GameManager.instance.mPerkCtrlData[i].Id)
                {
                    idxCtrl = i;
                    level = GameManager.instance.mPerkCtrlData[i].Level;
                }

            }
            switch (GameManager.instance.mPerkJsonData[mId].DescType)
            {
                case Enum.DescType.Perk:
                    if (level == 0)
                    {
                        idxCtrl = GameManager.instance.mPerkSize++;
                        GameManager.instance.mPerkCtrlData[idxCtrl].Id = idx;
                        GameManager.instance.mPerkCtrlData[idxCtrl].Level = 0;
                    }
                    FuncWeapon.UpdatePerkIdx(idx);
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
            if (GameManager.instance.mPerkCtrlData[idxCtrl].Level == GameManager.instance.mPerkJsonData[idx].Damage.Length)
            {
                GetComponent<Button>().interactable = false;
            }
        }


    }
}
