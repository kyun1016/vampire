using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 2. Ǯ ����� �ϴ� ����Ʈ��
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
        select = Instantiate(GameManager.instance.mPrefabs[mSpriteId], transform);
        mPool.Add(select);

        return select;
    }
}
