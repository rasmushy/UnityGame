using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Script : MonoBehaviour
{
    public GameObject fadeImage;

    //When player enters the level. This hides the black image. The image is used on the Fade animation
    private void OnTriggerExit2D(Collider2D collision)
    {  
        fadeImage.SetActive(false);
    }
}
