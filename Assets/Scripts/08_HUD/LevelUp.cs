using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform mRect;
    Item[] mItems;
    void Awake()
    {
        mRect = GetComponent<RectTransform>();
        mItems = GetComponentsInChildren<Item>(true);
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
        // 1. 모든 아이템 비활성화
        foreach(Item item in mItems)
        {
            item.gameObject.SetActive(false);
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
            Item ranItem = mItems[ran[index]];

            // 3. 만렙 아이템의 경우는 소비아이템으로 대체
            if(ranItem.mLevel == ranItem.mData.damages.Length)
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
