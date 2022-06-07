using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMainMenu : MonoBehaviour
{
    private HeroKnight heroPause;

    private GameObject cam;


    // At Start check that we dont have maincameras and heroknights duplicating.
    void Start() 
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        heroPause = FindObjectOfType<HeroKnight>();
        if (heroPause != null)
            heroPause.GetComponent<HeroKnight>().EndScreen();
        if (cam != null)
            Destroy(cam);
    }
        //Basic main menu. Not much to explain here
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        Debug.Log("Continue");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
   

}
