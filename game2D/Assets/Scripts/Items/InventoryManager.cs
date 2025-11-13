using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public Inventory myBag;
    public GameObject slotGrid;
    public Slot SlotPrefab;
    public Text itemInformation;
    private void OnEnable()
    {
        RefreshItem();
        instance.itemInformation.text = "";
    }

    public static void UpdateItemInfo(string itemDescription)//更新信息框内容
    {
        instance.itemInformation.text = itemDescription;
    }

    public static void CloseChosenBlock()//关闭所有的选中框
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
            instance.slotGrid.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    public static void CreateNewItem(Item item, int i)//创造背包物品-new
    {
        Slot newItem = instance.slotGrid.transform.GetChild(i).gameObject.GetComponent<Slot>();
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        instance.slotGrid.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(true);


    }
    public static void RefreshItem()//刷新物品-new
    {
        for (int i = 1; i < instance.slotGrid.transform.childCount; i++)
        {
            instance.slotGrid.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
            
        for (int i = 1; i < instance.slotGrid.transform.childCount; i++)
        {
            CreateNewItem(instance.myBag.itemList[i], i);
        }
            
    }
}
