using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class FuncPool
{

    public static void DropGold(int value, Transform pos)
    {
        GameObject item = GameManager.instance.mDropPool.Get();
        item.transform.position = pos.position;
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

    public static void DropExp(int value, Transform pos)
    {
        GameObject item = GameManager.instance.mDropPool.Get();
        item.transform.position = pos.position;
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
}
