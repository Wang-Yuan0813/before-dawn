using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public Item slotItem;
    public Image slotImage;
    public GameObject chosenBlock;
    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (EventSystem.current.currentSelectedGameObject == this.gameObject)
            {
                this.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
    public void ItemOnSelect()
    {
        InventoryManager.UpdateItemInfo(slotItem.itemInfo);
        //InventoryManager.CloseChosenBlock();
        chosenBlock.SetActive(true);
    }
    public void ItemOnUse()
    {
        Debug.Log("use this" + this.gameObject.name);
    }
    public void Deselect()
    {
        chosenBlock.SetActive(false);
    }
}
