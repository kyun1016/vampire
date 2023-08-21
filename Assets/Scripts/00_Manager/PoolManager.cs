using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 1. 프리펩들을 보관할 변수
    public GameObject[] mPrefabs;

    // 2. 풀 담당을 하는 리스트들
    List<GameObject>[] mPools;

    private void Awake()
    {
        mPools = new List<GameObject>[mPrefabs.Length];

        for(int i=0; i < mPools.Length; ++i)
        {
            mPools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ... 선택한 풀의 놀고 (비활성화 된) 있는 게임 오브젝트 접근
        foreach (GameObject item in mPools[index])
        {
            // ... 발견하면 select 변수에 할당
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                return select;
            }
        }

        // ... 못찾은 경우, 풀 등록
        select = Instantiate(mPrefabs[index], transform);
        mPools[index].Add(select);

        return select;
    }
}
