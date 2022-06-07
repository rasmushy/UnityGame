using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item 
{
   //Item Types
    public enum ItemType
    {
        HealthPotion,
        ManaPotion,
        Coin,
        DmgB,
        Book,
    }

    public ItemType itemType;
    public int amount;

    //Gets the Item Cost
    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion: return 30;
            case ItemType.ManaPotion: return 30;
            case ItemType.DmgB: return 150;
            case ItemType.Book: return 300;
                
        }
    }

    //Gets ItemSprites
    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion:      return ItemAssets.Instance.healthPotionSprite;
            case ItemType.ManaPotion:        return ItemAssets.Instance.manaPotionSprite;
            case ItemType.Coin:              return ItemAssets.Instance.coinSprite;
            case ItemType.DmgB:              return ItemAssets.Instance.dmgBSprite;
            case ItemType.Book:              return ItemAssets.Instance.bookSprite;

        }
    }

    //Check if the item is stackable or not
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.HealthPotion:
            case ItemType.ManaPotion:
            case ItemType.DmgB:
                return true;
        }
    }
}
