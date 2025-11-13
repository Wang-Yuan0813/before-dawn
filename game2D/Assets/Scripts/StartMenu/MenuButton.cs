using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject chosenBlock;
    public GameObject cameraFocus;

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

    private void ButtonOnSelect()//选取出现选取框
    {
        StartMenu.CloseChosenBlock();
        chosenBlock.SetActive(true);
        Color color = chosenBlock.transform.parent.GetComponent<Image>().color;
        color.a = 1f;
        chosenBlock.transform.parent.GetComponent<Image>().color = color;
    }
    public void SelectStart()
    {
        ButtonOnSelect();
        cameraFocus.transform.position = new Vector3(-10,-5, 0);
    }
    public void SelectOptions()
    {
        ButtonOnSelect();
        cameraFocus.transform.position = new Vector3(-5, -5, 0);
    }
    public void SelectCredits()
    {
        ButtonOnSelect();
        cameraFocus.transform.position = new Vector3(0, -5, 0);
    }
    public void SelectExit()
    {
        ButtonOnSelect();
        cameraFocus.transform.position = new Vector3(5, -5, 0);
    }
    public void StartGame()
    {
        Debug.Log("start ");
        SceneManager.LoadScene(1);
    }
    public void Options()
    {
        Debug.Log("Options this");
    }
    public void Credits()
    {
        Debug.Log("Credits this");
    }
    public void Exit()
    {
        Debug.Log("Exit this");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
