using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform mRect;
    HUDItem[] mItems;
    void Awake()
    {
        mRect = GetComponent<RectTransform>();
        mItems = GetComponentsInChildren<HUDItem>(true);
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
        mItems[index].OnClick();
    }

    void Next()
    {
        // 1. ��� ������ ��Ȱ��ȭ
        foreach(HUDItem item in mItems)
        {
            item.gameObject.SetActive(false);
        }

        // 2. �� �߿��� ���� 3�� ������ Ȱ��ȭ
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
            // 3. ���� �������� ���� �Һ���������� ��ü
            HUDItem ranItem = mItems[ran[index]];
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
                    mItems[mItems.Length - 1].gameObject.SetActive(true);
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
                    mItems[mItems.Length - 1].gameObject.SetActive(true);
                    return;
                }
            }
            ranItem.gameObject.SetActive(true);
        }
    }
}
