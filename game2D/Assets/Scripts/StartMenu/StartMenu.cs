using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class StartMenu : MonoBehaviour
{
    static StartMenu instance;
    public GameObject firstChose;
    public GameObject buttons;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;

    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);//清除选中的对象
        EventSystem.current.SetSelectedGameObject(firstChose);
    }

    public static void CloseChosenBlock()//关闭所有的选中框
    {
        for (int i = 0; i < instance.buttons.transform.childCount; i++)
        {
            instance.buttons.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Color color = instance.buttons.transform.GetChild(i).gameObject.GetComponent<Image>().color;
            color.a = 0.2f;
            instance.buttons.transform.GetChild(i).gameObject.GetComponent<Image>().color = color;
        }

    }
}
