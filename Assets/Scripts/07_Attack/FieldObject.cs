using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObject : MonoBehaviour
{
    public Enum.FieldObjectSprite mType;

    public void Init(int value)
    {
        mType = (Enum.FieldObjectSprite) value;
        gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mFieldObjectSprite[value];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mType == Enum.FieldObjectSprite.Box)
            if (collision.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                return;
            }

        if (collision.CompareTag("Melee") || collision.CompareTag("Range"))
        {
            GameObject item = GameManager.instance.mDropPool.Get();
            item.transform.position = transform.position;
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
            gameObject.SetActive(false);
            return;
        }
    }
}
