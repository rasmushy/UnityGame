using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausemenu;
    public bool isPaused;

    private HeroKnight heroPause;

    //This class checks if player presses ESC button. When pressed. Time scale goes to 0 so nothing is moving.
    //When Resume Game button is pressed, changes the time scale to 1 so everything moves now. Also hides the UI 

    void Start()
    {
        pausemenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
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
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        heroPause = FindObjectOfType<HeroKnight>();
        if (heroPause != null)
            heroPause.GetComponent<HeroKnight>().EndScreen();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

}
