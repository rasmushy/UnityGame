using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelControl : MonoBehaviour
{

    public int index;
    public string levelName;
    public GameObject fadeImage;
    public Image black;
    public Animator anim;
    public bool travel;


    //This class checks if player is standing in Doors trigger box. If player presses W when inside the trigger box, it loads a level. 
    //The level is told by the index number
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        travel = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        fadeImage.SetActive(false);
        travel = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && travel == true)
        {
            fadeImage.SetActive(true);
            StartCoroutine(Fading());
        }     
    }
    //Plays the Fading animation when player changes the level.
    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(index);
    }

}
