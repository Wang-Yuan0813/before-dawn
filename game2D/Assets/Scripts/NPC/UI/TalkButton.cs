using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TalkButton : MonoBehaviour
{
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

    public void SelectThisChoice()
    {
        DialogueManager.GetInstance().CloseAllButtonChosenBlock();
        Debug.Log("选中的按钮：" + this.transform.Find("Text").gameObject.GetComponent<Text>().text);

        this.transform.Find("ChosenBlock").gameObject.SetActive(true);
        Color buttonColor = this.GetComponent<Image>().color;
        this.GetComponent<Image>().color = new Color(buttonColor.r, buttonColor.g, buttonColor.b, 0.5f);
    }
}
