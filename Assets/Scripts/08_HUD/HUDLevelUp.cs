using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDLevelUp : MonoBehaviour
{
    RectTransform mRect;
    public GameObject mRootHUD;
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
        mItems = new GameObject[GameManager.instance.mJsonWeaponData.Length + GameManager.instance.mJsonPerkData.Length];
        mItems[0] = mRootHUD;
        for (int i = 1; i < mItems.Length; ++i)
        {
            mItems[i] = Instantiate(mRootHUD);
            mItems[i].GetComponent<HUDBtnItem>().mId = i;
            mItems[i].transform.SetParent(mRootHUD.transform.parent);
            mItems[i].transform.name = "Item " + i;
            mItems[i].transform.localScale = mRootHUD.transform.localScale;
        }
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
        Debug.Log(up.ToString() +" / " + mSelNum.ToString());

        if(mSelNum == -1)
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

        if(up)
        {
            if (mSelNum == mItems.Length)
                return;
            for(mSelNum=mSelNum+1; mSelNum< mItems.Length; ++mSelNum)
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
            if (mSelNum == 0)
                return;
            for (mSelNum = mSelNum - 1; mSelNum > 0; --mSelNum)
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
        // 1. 모든 아이템 비활성화
        foreach(GameObject item in mItems)
        {
            item.SetActive(false);
        }

        // 2. 그 중에서 랜덤 3개 아이템 활성화
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, mItems.Length);
            ran[1] = Random.Range(0, mItems.Length);
            ran[2] = Random.Range(0, mItems.Length);
            if (ran[0] != ran[1] && ran[0] != ran[2] && ran[1] != ran[2])
                break;
        }
        for (int index = 0; index < ran.Length; ++index) 
        {
            // 3. 만렙 아이템의 경우는 소비아이템으로 대체
            GameObject ranItem = mItems[ran[index]];
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
                    mItems[mItems.Length - 1].SetActive(true);
                    return;
                }
                if ((GameManager.instance.mPlayerData.WeaponSize == GameManager.instance.mJsonPlayerData[GameManager.instance.mPlayerData.Id].MaxWeaponSize) &&
                    (level == -1))
                {
                    mItems[mItems.Length - 1].SetActive(true);
                    return;
                }
            }
            else if(idx < GameManager.instance.mJsonWeaponData.Length + GameManager.instance.mJsonPerkData.Length)
            {
                idx -= GameManager.instance.mJsonWeaponData.Length;
                for (int i = 0; i < GameManager.instance.mPlayerData.PerkSize; ++i)
                {
                    if (idx == GameManager.instance.mPerkCtrlData[i].Id)
                        level = GameManager.instance.mPerkCtrlData[i].Level;
                }

                if (level == GameManager.instance.mJsonPerkData[idx].Damage.Length)
                {
                    mItems[mItems.Length - 1].SetActive(true);
                    return;
                }
                if ((GameManager.instance.mPlayerData.PerkSize == GameManager.instance.mJsonPlayerData[GameManager.instance.mPlayerData.Id].MaxPerkSize) && 
                    (level == -1))
                {
                    mItems[mItems.Length - 1].SetActive(true);
                    return;
                }
            }
            ranItem.SetActive(true);
        }
    }
}
