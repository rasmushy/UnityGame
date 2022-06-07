using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class UI_Inventory : MonoBehaviour
{

    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    public bool isOpen;
    public bool invenOpen;
    public GameObject inve;
    public GameObject item;
    public GameObject UI;
    private HeroKnight player;


    public void SetPlayer (HeroKnight player)
    {
        this.player = player;
    }
    //Checks if the player presses tab, if yes, shows the inventory, when pressed again, hides the inventory UI
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            if (isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }
    public void Open()
    {
        item.SetActive(true);
        inve.SetActive(true);
        UI.SetActive(true);
        isOpen = true;
    }

    public void Close()
    {
        item.SetActive(false);
        inve.SetActive(false);
        UI.SetActive(false);
        isOpen = false;
    }
    private void Awake()
    {
        //Finds the template for the inventory
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventory();
    }

    //Refreshes the inventory if items are added to the inventory
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventory();
    }

    private void RefreshInventory()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 153f;
        foreach (Item item in inventory.GetItemList())
        {
            //Makes copy of the template
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                // Use item
                inventory.UseItem(item);
            };
            //Gets Sprite image of the item and the amount
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = Item.GetSprite(item.itemType);
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();

            if (item.amount > 1)
            {             
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            //Adds ónly 4-non stackable items in a row and then moves under
            x++;
            if (x > 5)
            {
                x = 0;
                y++;
            }
        }
    }
}
