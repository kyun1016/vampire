using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 2. Ǯ ����� �ϴ� ����Ʈ��
    public List<GameObject> mPool;
    int mPoolId;

    public void Init(int id)
    {
        mPoolId = id;
        mPool = new List<GameObject>();
        mPool.Add(Instantiate(GameManager.instance.mPoolPrefabs[mPoolId], transform));
        mPool[0].SetActive(false);
    }

    public GameObject Get()
    {
        GameObject select = null;

        // ... ������ Ǯ�� ��� (��Ȱ��ȭ ��) �ִ� ���� ������Ʈ ����
        foreach (GameObject item in mPool)
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
        select = Instantiate(mPool[0], transform);
        select.name = mPool[0].name;
        select.SetActive(true);
        mPool.Add(select);

        return select;
    }
}
