using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDLevelUp : MonoBehaviour
{
    RectTransform mRect;
    public GameObject mRootHUD;
    GameObject[] mItems;
    void Awake()
    {
        mRect = GetComponent<RectTransform>();
        mItems = new GameObject[GameManager.instance.mWeaponJsonData.Length + GameManager.instance.mPerkJsonData.Length];
        mItems[0] = mRootHUD;
        for (int i = 1; i < mItems.Length; ++i)
        {
            mItems[i] = Instantiate(mRootHUD);
            mItems[i].GetComponent<HUDBtnItem>().mId = i;
            mItems[i].transform.SetParent(mRootHUD.transform.parent);
            mItems[i].transform.name = "Item " + i;
            mItems[i].transform.localScale = mRootHUD.transform.localScale;
        }    
    }

    public void Show()
    {
        Next();
        GameManager.instance.Stop();
        mRect.localScale = Vector3.one;
    }

    public void Hide()
    {
        GameManager.instance.Resume();
        mRect.localScale = Vector3.zero;
    }

    public void Select(int index)
    {
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
            if (idx < GameManager.instance.mWeaponJsonData.Length)
            {
                for (int i = 0; i < GameManager.instance.mPlayerData.WeaponSize; ++i)
                {
                    if (idx == GameManager.instance.mWeaponCtrlData[i].Id)
                        level = GameManager.instance.mWeaponCtrlData[i].Level;
                }

                if (level == GameManager.instance.mWeaponJsonData[idx].Damage.Length)
                {
                    mItems[mItems.Length - 1].SetActive(true);
                    return;
                }
                if ((GameManager.instance.mPlayerData.WeaponSize == GameManager.instance.mPlayerJsonData[GameManager.instance.mPlayerData.Id].MaxWeaponSize) &&
                    (level == -1))
                {
                    mItems[mItems.Length - 1].SetActive(true);
                    return;
                }
            }
            else if(idx < GameManager.instance.mWeaponJsonData.Length + GameManager.instance.mPerkJsonData.Length)
            {
                idx -= GameManager.instance.mWeaponJsonData.Length;
                for (int i = 0; i < GameManager.instance.mPlayerData.PerkSize; ++i)
                {
                    if (idx == GameManager.instance.mPerkCtrlData[i].Id)
                        level = GameManager.instance.mPerkCtrlData[i].Level;
                }

                if (level == GameManager.instance.mPerkJsonData[idx].Damage.Length)
                {
                    mItems[mItems.Length - 1].SetActive(true);
                    return;
                }
                if ((GameManager.instance.mPlayerData.PerkSize == GameManager.instance.mPlayerJsonData[GameManager.instance.mPlayerData.Id].MaxPerkSize) && 
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
