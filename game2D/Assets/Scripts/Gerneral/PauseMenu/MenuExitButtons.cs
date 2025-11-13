using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuExitButtons : MonoBehaviour
{
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
    public void SelectExitGame()
    {
        Debug.Log("SelectExitGame");
        ExitPanelManager.ClosedAllChosenBlock();
        chosenBlock.SetActive(true);
    }
    public void SelectQuitToStart()
    {
        Debug.Log("SelectQuitToStart");
        ExitPanelManager.ClosedAllChosenBlock();
        chosenBlock.SetActive(true);
    }
    public void ClickExitGame()
    {
        Debug.Log("ClickExitGame");
        Debug.Log("Exit this");
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
    public void ClickQuitToStart()
    {
        Debug.Log("ClickQuitToStart");
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
