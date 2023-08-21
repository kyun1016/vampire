using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 1. ��������� ������ ����
    public GameObject[] mPrefabs;

    // 2. Ǯ ����� �ϴ� ����Ʈ��
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

        // ... ������ Ǯ�� ��� (��Ȱ��ȭ ��) �ִ� ���� ������Ʈ ����
        foreach (GameObject item in mPools[index])
        {
            // ... �߰��ϸ� select ������ �Ҵ�
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                return select;
            }
        }

        // ... ��ã�� ���, Ǯ ���
        select = Instantiate(mPrefabs[index], transform);
        mPools[index].Add(select);

        return select;
    }
}
