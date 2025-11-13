using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuButton : MonoBehaviour
{

    public GameObject bagPanel;
    public GameObject settingsPanel;
    public GameObject exitPanel;
    public GameObject chosenBlock;
    public void SelectBag()
    {
        CloseAllPanel();
        bagPanel.SetActive(true);
        MenuListManager.ClosedAllChosenBlock();
        chosenBlock.SetActive(true);
    }
    public void SelectSettings()
    {
        CloseAllPanel();
        settingsPanel.SetActive(true);
        MenuListManager.ClosedAllChosenBlock();
        chosenBlock.SetActive(true);
    }
    public void SelectExit()
    {
        CloseAllPanel();
        exitPanel.SetActive(true);
        MenuListManager.ClosedAllChosenBlock();
        chosenBlock.SetActive(true);
    }
    private void CloseAllPanel()
    {
        bagPanel.SetActive(false);
        settingsPanel.SetActive(false);
        exitPanel.SetActive(false);
    }

}
