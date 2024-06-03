using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 2. 풀 담당을 하는 리스트들
    public List<GameObject> mPool;
    int mSpriteId;

    public void Init(int id)
    {
        mSpriteId = id;
        mPool = new List<GameObject>();
    }

    public GameObject Get()
    {
        GameObject select = null;

        // ... 선택한 풀의 놀고 (비활성화 된) 있는 게임 오브젝트 접근
        foreach (GameObject item in mPool)
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
        select = Instantiate(GameManager.instance.mPrefabs[mSpriteId], transform);
        mPool.Add(select);

        return select;
    }
}
