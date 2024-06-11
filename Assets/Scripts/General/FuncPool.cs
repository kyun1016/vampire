using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class FuncPool
{
    public static void SpawnEnemy(int level)
    {
        GameObject enemy = GameManager.instance.mEnemyPool.Get();
        enemy.transform.position = GameManager.instance.mPlayer.transform.position;
        Vector3 pos = Random.insideUnitCircle;
        if (pos.magnitude == 0)
            pos.x = 1;
        pos = pos.normalized;
        enemy.transform.position += pos * 20;
        enemy.GetComponent<Enemy>().Init(level);
    }

    public static void SpawnFieldObject()
    {
        GameObject item = GameManager.instance.mFieldObjectPool.Get();
        item.transform.position = GameManager.instance.mPlayer.transform.position;
        Vector3 pos = Random.insideUnitCircle;
        if (pos.magnitude == 0)
            pos.x = 1;
        pos = pos.normalized;
        item.transform.position += pos * 20;
        int value = Random.Range(0, GameManager.instance.mFieldObjectSprite.Length);
        item.GetComponent<FieldObject>().Init(value);
    }

    public static void DropGold(int value, Vector3 pos)
    {
        GameObject item = GameManager.instance.mDropPool.Get();
        item.transform.position = pos;
        item.transform.position += new Vector3(Random.value * 2 - 1, Random.value * 2 - 1);
        item.transform.rotation = Quaternion.identity;
        item.GetComponent<DropItem>().mData = value;
        switch (value)
        {
            case > 50:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Gold2;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Gold2];
                break;
            case > 10:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Gold1;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Gold1];
                break;
            default:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Gold0;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Gold0];
                break;
        }
        item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

    public static void DropExp(int value, Vector3 pos)
    {
        GameObject item = GameManager.instance.mDropPool.Get();
        item.transform.position = pos;
        item.transform.rotation = Quaternion.identity;
        item.GetComponent<DropItem>().mData = value;
        switch (value)
        {
            case > 50:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Exp2;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Exp2];
                break;
            case > 10:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Exp1;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Exp1];
                break;
            default:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Exp0;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Exp0];
                break;
        }
        item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

    public static void DropItem(Vector3 pos)
    {
        GameObject item = GameManager.instance.mDropPool.Get();
        item.transform.position = pos;
        item.transform.rotation = Quaternion.identity;

        float val = Random.value;
        switch (val)
        {
            case < 0.1f:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Mag;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Mag];
                break;
            case < 0.3f:
                item.GetComponent<DropItem>().mType = Enum.DropItemSprite.Health;
                item.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mDropItemSprite[(int)Enum.DropItemSprite.Health];
                break;
        }
    }
}
