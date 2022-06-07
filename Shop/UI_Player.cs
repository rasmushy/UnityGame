using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Player : MonoBehaviour
{

    private TextMeshProUGUI goldText;
    private TextMeshProUGUI healthPotionText;


    //This class checks if player gets gold from enemies, and updates it on the inventory UI

    private void Awake()
    {
        goldText = transform.Find("goldText").GetComponent<TextMeshProUGUI>();   
    }

    private void Start()
    {
        HeroKnight.Instance.OnGoldAmountChanged += Instance_OnGoldAmountChanged; 
    }
    private void Update()
    {
        UpdateText();
    }


    private void Instance_OnGoldAmountChanged(object sender, System.EventArgs e)
    {
        UpdateText();
    }

    private void UpdateText()
    {
        goldText.text = HeroKnight.Instance.GetGoldAmount().ToString();
        
    }

}
