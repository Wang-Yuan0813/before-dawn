using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuListManager : MonoBehaviour
{
    static MenuListManager instance;
    public GameObject buttons;
    public GameObject panels;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    public static void ClosedAllChosenBlock()
    {
        for(int i = 0;i < instance.buttons.transform.childCount; i++)
        {
            GameObject thisButton = instance.buttons.transform.GetChild(i).gameObject;
            thisButton.transform.Find("ChosenBlock").gameObject.SetActive(false);
        }
    }
}
