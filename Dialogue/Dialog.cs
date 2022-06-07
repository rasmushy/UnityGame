using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{

    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public Image black;
    public Animator anim; 
    public GameObject continueButton;


    private void Start()
    {    
        StartCoroutine(St());     
        StartCoroutine(Type());
    }

    private void Update()
    {
        //After every sentence the continue button shows
        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }
    //After all of the sentences are been through, the fade animation plays, and Village level loads
    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene("Village");
    }
    //Waits 3 second before sentences start coming
    IEnumerator St()
    {
        yield return new WaitForSeconds(3);      
    }   
    //Starts typing text on the screen, and waits for the typingspeed.
    IEnumerator Type()
    {      
        foreach (char letter in sentences[index].ToCharArray())
      {           
         textDisplay.text += letter;
         yield return new WaitForSeconds(typingSpeed);
      }       
    }
    //Tells what continue button does, if there is still sentences to be displayed, continue button starts the next sentence.
    //After the last one it displays no text on screen, and starts the Fading Method above
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
            StartCoroutine(Fading());
            textDisplay.text = "";
            continueButton.SetActive(false);           
        }       
    }
}
