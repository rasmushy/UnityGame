using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelPoint : MonoBehaviour
{
    public GameObject map;
    public GameObject tutText;
    public bool travelPoint;
    public bool isOpen;
    void Start()
    {
        //Puts the UI to false
        map.SetActive(false);
        tutText.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && travelPoint == true)
        {
           //Check if the player is on the travelpoint trigger box, and if player presses tab on the trigger box 
            if (isOpen)
            {
                CloseMap();
            }
            else
            {
                OpenMap();
            }
        }
    }
    //When player stands on the trigger box, shows the tutorial text and sends a travelpoint true to the update method
    private void OnTriggerEnter2D(Collider2D collision)
    {
        travelPoint = true;
        tutText.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        travelPoint = false;
        tutText.SetActive(false);
    }
    public void OpenMap()
    {
        map.SetActive(true);
        Time.timeScale = 0f;
        isOpen = true;
    }
    public void CloseMap()
    {
        map.SetActive(false);
        Time.timeScale = 1f;
        isOpen = false;
    }

    public void Cave()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");      
        isOpen = false;
    }
    public void Forest()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level4");       
        isOpen = false;
    }
    public void Village()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Village");       
        isOpen = false;
    }
    public void VillageNight()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("VillageNight");        
        isOpen = false;
    }
}
