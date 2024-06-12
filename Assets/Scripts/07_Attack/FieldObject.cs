using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObject : MonoBehaviour
{
    public Enum.FieldObjectSprite mType;

    public void Init(int value)
    {
        mType = (Enum.FieldObjectSprite)value;
        gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.mFieldObjectSprite[value];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mType == Enum.FieldObjectSprite.Box)
        {
            if (collision.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                return;
            }
            
        }
        else 
        {
            if (collision.CompareTag("Weapon"))
            {
                FuncPool.DropItem(transform.position);
                gameObject.SetActive(false);
                return;
            }
        }
    }
}
