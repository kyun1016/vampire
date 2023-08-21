using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] mSpawnPoint;

    float mTimer;
    private void Awake()
    {
        mSpawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        mTimer += Time.deltaTime;

        if(mTimer > 0.2f)
        {
            mTimer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.mInstance.mPoolManager.Get(Random.Range(0,2));
        enemy.transform.position = mSpawnPoint[Random.Range(1, mSpawnPoint.Length)].position;
    }
}
