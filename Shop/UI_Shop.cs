using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class UI_Shop : MonoBehaviour
{
   
    private Transform container;
    private Transform shopItemTemplate;
    private IShopCustomer shopCustomer;

    private void Awake()
    {
        //Finds container and the premade template. Also hides the template
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        //Creates Shop Buttons for each item, gets the image and cost from Item class
        CreateItemButton(Item.ItemType.HealthPotion, Item.GetSprite(Item.ItemType.HealthPotion), "Health Potion", Item.GetCost(Item.ItemType.HealthPotion), 0);
        CreateItemButton(Item.ItemType.DmgB, Item.GetSprite(Item.ItemType.DmgB), "Damage Boost", Item.GetCost(Item.ItemType.DmgB), 1);
        CreateItemButton(Item.ItemType.Book, Item.GetSprite(Item.ItemType.Book), "Spell Book", Item.GetCost(Item.ItemType.Book), 2);
        Hide();
    }
    private void CreateItemButton(Item.ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        //Gets the Template, and modivies it to get the current shop items and gets item info
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        shopItemTransform.gameObject.SetActive(true);
        float shopItemHeight = 200f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);
        //Changes the text and images
        shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;
        //Clicking on a Item in shop
        shopItemTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            Debug.Log("Clicks");
            TryBuyItem(itemType);
        };
    }

    //Tries to buy item
    private void TryBuyItem(Item.ItemType itemType)
    {
        if (shopCustomer.TrySpendGoldAmount(Item.GetCost(itemType)))
        {
            //Can afford
            shopCustomer.BoughtItem(itemType);
            Debug.Log("Ostettu" + itemType);
            
        }
        
    }

    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
