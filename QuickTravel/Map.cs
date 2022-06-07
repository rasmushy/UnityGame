using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{

    public GameObject map;

    public bool isOpen;
    void Start()
    {
        map.SetActive(false);
    }

    //Checks if player presses M, then shows Map.
    //NOT USED. WE ARE USING TRAVEL POINT SCRIPT FOR THIS.

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isOpen)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
       map.SetActive(true);
        Time.timeScale = 0f;
        isOpen = true;
    }
    public void ResumeGame()
    {
        map.SetActive(false);
        Time.timeScale = 1f;
        isOpen = false;
    }

   public void Cave()
    {
        SceneManager.LoadScene("Level2");
        Time.timeScale = 1f;
        isOpen = false;
    }
    public void Forest()
    {
        SceneManager.LoadScene("Level3");
        Time.timeScale = 1f;
        isOpen = false;
    }
}
