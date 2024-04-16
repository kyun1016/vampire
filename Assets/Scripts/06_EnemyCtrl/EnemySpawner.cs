using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] mSpawnPoint;
    public EnemyData[] mEnemyData;


    int mLevel;
    float mTimer;
    private void Awake()
    {
        mSpawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        if (!GameManager.instance.mIsLive)
            return;

        mTimer += Time.deltaTime;
        mLevel = Mathf.FloorToInt(GameManager.instance.mGameTime / 10f);
        mLevel = mLevel > mEnemyData.Length ? mEnemyData.Length : mLevel;
        if (mTimer > mEnemyData[mLevel].spawnTime)
        {
            mTimer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.mPoolManager.Get(0);
        enemy.transform.position = mSpawnPoint[Random.Range(1, mSpawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(mEnemyData[mLevel]);
    }
}

