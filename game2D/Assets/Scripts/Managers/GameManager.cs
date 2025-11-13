using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool playerCanMove;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("GameManager instance created.");
        }
        else
        {
            Debug.LogWarning("GameManager instance already exists.");
        }
    }
    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("GameManager instance is null.");
        }
        return instance;
    }
    public void PauseGame(bool pause)
    {
        if (pause)
        {
            playerCanMove = false;
            Time.timeScale = 0;
            Debug.Log("Game Paused");
        }
        else
        {
            playerCanMove = true;
            Time.timeScale = 1;
            Debug.Log("Game Resumed");
        }
    }
    public void PlayerIsInteracting(bool isInteracting)
    {
        if (isInteracting)
        {
            playerCanMove = false;
        }
        else
        {
            playerCanMove = true;
        }
    }
}
