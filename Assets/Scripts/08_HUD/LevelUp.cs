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
            HUDItem ranItem = mItems[ran[index]];

            // 3. ���� �������� ���� �Һ���������� ��ü
            if(GameManager.instance.mItemLevel[ranItem.mId] == GameManager.instance.mItemData[ranItem.mId].Damages.Length)
            {
                mItems[4].gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }


    }
}
