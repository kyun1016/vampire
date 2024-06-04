using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public Transform[] mSpawnPoint;

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
        mLevel = Mathf.FloorToInt(GameManager.instance.mPlayerData.GameTime / 10f);
        mLevel = mLevel >= GameManager.instance.mEnemyJsonData.Length ? GameManager.instance.mEnemyJsonData.Length - 1 : mLevel;
        if (mTimer > GameManager.instance.mEnemyJsonData[mLevel].SpawnTime)
        {
            mTimer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.mEnemyPool.Get();
        enemy.transform.position = mSpawnPoint[Random.Range(1, mSpawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(mLevel);
    }
}

