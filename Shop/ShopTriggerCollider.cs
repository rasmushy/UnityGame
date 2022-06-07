using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerCollider : MonoBehaviour
{
    [SerializeField] private UI_Shop uiShop;

    //Checks if the player is standing in the shop trigger box, if yes, checks if the player is standing in the triggerbox, the shop ui shows
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IShopCustomer shopCustomer = collision.GetComponent<IShopCustomer>();
        if(shopCustomer != null)
        {
            uiShop.Show(shopCustomer);
        }
    }
    //When player leaves the box trigger box, the ui goes away
    private void OnTriggerExit2D(Collider2D collision)
    {
        IShopCustomer shopCustomer = collision.GetComponent<IShopCustomer>();
        if (shopCustomer != null)
        {
            uiShop.Hide();
        }
    }
}
