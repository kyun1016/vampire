using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDLevelUp : MonoBehaviour
{
    RectTransform mRect;
    public GameObject[] mItems;
    public TMP_Text mTextTitle;
    public int mSelNum;


    public void UpdateText()
    {
        mTextTitle.text = GameManager.instance.mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].HUDLevelUpTitle[0];
        for (int i = 0; i < mItems.Length; ++i)
        {
            mItems[i].GetComponent<HUDBtnItem>().UpdateText();
        }
    }

    public void Init()
    {
        mSelNum = -1;
    }

    public void Show()
    {
        mTextTitle.text = GameManager.instance.mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].HUDLevelUpTitle[0];
        Next();
        GameManager.instance.mPlayer.mInputVec = Vector2.zero;
        GameManager.instance.Stop();

        GameManager.instance.PlayEffect(true);
        GameManager.instance.PlaySFX(Enum.SFX.LevelUp);
    }

    public void Hide()
    {
        GameManager.instance.Resume();

        GameManager.instance.PlayEffect(false);
        GameManager.instance.PlaySFX(Enum.SFX.Select);
        gameObject.SetActive(false);
    }

    public void SelectBtn(bool up)
    {
        // Debug.Log(up.ToString() +" / " + mSelNum.ToString());

        if (mSelNum == -1)
        {
            for (mSelNum = 0; mSelNum < mItems.Length; ++mSelNum)
            {
                if (mItems[mSelNum].gameObject.activeSelf)
                {
                    mItems[mSelNum].gameObject.GetComponent<Button>().Select();
                    return;
                }
            }
        }

        if (!up)
        {
            if (mSelNum >= mItems.Length-1)
            {
                mSelNum = 0;
                mItems[mSelNum].gameObject.GetComponent<Button>().Select();
                return;
            }
            for (mSelNum = mSelNum + 1; mSelNum < mItems.Length; ++mSelNum)
            {
                if (mItems[mSelNum].gameObject.activeSelf)
                {
                    mItems[mSelNum].gameObject.GetComponent<Button>().Select();
                    return;
                }
            }
        }
        else
        {
            if (mSelNum <= 0)
            {
                mSelNum = mItems.Length - 1;
                mItems[mSelNum].gameObject.GetComponent<Button>().Select();
                return;
            }
            for (mSelNum = mSelNum - 1; mSelNum >= 0; --mSelNum)
            {
                if (mItems[mSelNum].gameObject.activeSelf)
                {
                    mItems[mSelNum].gameObject.GetComponent<Button>().Select();
                    return;
                }
            }
        }
    }

    public void Select(int index)
    {
        mSelNum = -1;
        mItems[index].GetComponent<HUDBtnItem>().OnClick();
    }

    void Next()
    {
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, GameManager.instance.mJsonWeaponData.Length + GameManager.instance.mJsonPerkData.Length);
            ran[1] = Random.Range(0, GameManager.instance.mJsonWeaponData.Length + GameManager.instance.mJsonPerkData.Length);
            ran[2] = Random.Range(0, GameManager.instance.mJsonWeaponData.Length + GameManager.instance.mJsonPerkData.Length);
            if (ran[0] != ran[1] && ran[0] != ran[2] && ran[1] != ran[2])
                break;
        }

        bool disable = false;
        for (int index = 0; index < ran.Length; ++index)
        {
            // 3. 만렙 아이템의 경우는 소비아이템으로 대체
            int level = -1;
            int idx = ran[index];
            if (idx < GameManager.instance.mJsonWeaponData.Length)
            {
                for (int i = 0; i < GameManager.instance.mPlayerData.WeaponSize; ++i)
                {
                    if (idx == GameManager.instance.mWeaponCtrlData[i].Id)
                        level = GameManager.instance.mWeaponCtrlData[i].Level;
                }

                if (level == GameManager.instance.mJsonWeaponData[idx].Damage.Length)
                {
                    disable = true;
                }
                if ((GameManager.instance.mPlayerData.WeaponSize == GameManager.instance.mJsonPlayerData[GameManager.instance.mPlayerData.Id].MaxWeaponSize) &&
                    (level == -1))
                {
                    disable = true;
                }
            }
            else if (idx < GameManager.instance.mJsonWeaponData.Length + GameManager.instance.mJsonPerkData.Length)
            {
                idx -= GameManager.instance.mJsonWeaponData.Length;
                for (int i = 0; i < GameManager.instance.mPlayerData.PerkSize; ++i)
                {
                    if (idx == GameManager.instance.mPerkCtrlData[i].Id)
                        level = GameManager.instance.mPerkCtrlData[i].Level;
                }

                if (level == GameManager.instance.mJsonPerkData[idx].Damage.Length)
                {
                    disable = true;
                }
                if ((GameManager.instance.mPlayerData.PerkSize == GameManager.instance.mJsonPlayerData[GameManager.instance.mPlayerData.Id].MaxPerkSize) &&
                    (level == -1))
                {
                    disable = true;
                }
            }

            if (disable)
            {
                mItems[index].GetComponent<HUDBtnItem>().Init(GameManager.instance.mJsonWeaponData.Length + GameManager.instance.mJsonPerkData.Length - 1);
            }
            else
            {
                mItems[index].GetComponent<HUDBtnItem>().Init(ran[index]);
            }

        }
    }
}
