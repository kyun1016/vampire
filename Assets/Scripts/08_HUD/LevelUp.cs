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
            int level = 0;
            int idx = ran[index];
            if (ran[index] < GameManager.instance.mWeaponJsonData.Length)
            {
                for (int i = 0; i < GameManager.instance.mWeaponSize; ++i)
                {
                    if (ran[index] == GameManager.instance.mWeaponCtrlData[i].Id)
                        level = GameManager.instance.mWeaponCtrlData[i].Level;
                }

                if (level == GameManager.instance.mWeaponJsonData[ranItem.mId].Damage.Length)
                {
                    mItems[4].gameObject.SetActive(true);
                    return;
                }
            }
            else
            {
                idx -= GameManager.instance.mWeaponJsonData.Length;
                for (int i = 0; i < GameManager.instance.mPerkSize; ++i)
                {
                    if (idx == GameManager.instance.mPerkCtrlData[i].Id)
                        level = GameManager.instance.mWeaponCtrlData[i].Level;
                }

                if (level == GameManager.instance.mPerkJsonData[idx].Damage.Length)
                {
                    mItems[4].gameObject.SetActive(true);
                    return;
                }
            }
            ranItem.gameObject.SetActive(true);
        }
    }
}
