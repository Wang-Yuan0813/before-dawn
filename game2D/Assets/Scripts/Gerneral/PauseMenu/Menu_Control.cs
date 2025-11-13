using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu_Control : MonoBehaviour
{
    public GameObject pauseMenuBackground;
    public GameObject menuList;
    public GameObject bagOption;
    public GameObject exitOption;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();//获取GameManager
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();//获取GameManager
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            SwitchPause(exitOption);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SwitchPause(bagOption);
        }
    }
    private void SwitchPause(GameObject selectOption)
    {
        if (!pauseMenuBackground.activeSelf)
        {
            pauseMenuBackground.SetActive(true);
            menuList.SetActive(true);
            gameManager.PauseGame(true);
            EventSystem.current.SetSelectedGameObject(null);//清除选中的对象
            EventSystem.current.SetSelectedGameObject(selectOption);

        }
        else
        {
            pauseMenuBackground.SetActive(false);
            menuList.SetActive(false);
            gameManager.PauseGame(false);
        }
    }
}
