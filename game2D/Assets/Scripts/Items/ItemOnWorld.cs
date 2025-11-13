using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Inventory playInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }

    }
    public void AddNewItem()
    {
        if (!playInventory.itemList.Contains(thisItem))
        {
            for(int i = 1; i < 8; i++)//因为只有8个槽位，偷懒
            {
                if (playInventory.itemList[i].isEmpty)//替换
                {
                    playInventory.itemList[i] = thisItem;
                    break;
                }
            }
            InventoryManager.RefreshItem();
        }
    }
}
