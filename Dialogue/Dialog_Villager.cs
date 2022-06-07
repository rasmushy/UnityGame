using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Dialog_Villager : MonoBehaviour
{
   
    public GameObject Dialog;
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject continueButton;
    public bool villager;
    public bool isOpen;

    // THIS CLASS IS THE SAME AS Dialogue CLASS WITH A FEW MODIFICATIONS

    void Start()
    {
        Dialog.SetActive(false);
    }

    void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
        //Checks if the player is next to the villager and presses T on the trigger box. When player leaves the area, the text hides
        if (Input.GetKeyDown(KeyCode.T) && villager == true)
        {
            textDisplay.text = "";
            if (isOpen)
            {
                Leaving();
            }
            else
            {
                StartCoroutine(Type());
                Talking();
            }
        }
    }
    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        villager = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Dialog.SetActive(false);
        isOpen = false;
        villager = false;
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            Dialog.SetActive(false);
            isOpen = false;
            villager = false;
        }

    }
    public void Talking()
    {     
        Dialog.SetActive(true);
        isOpen = true;      
    }
    public void Leaving()
    {
        Dialog.SetActive(false);    
        isOpen = false;
    }
}
