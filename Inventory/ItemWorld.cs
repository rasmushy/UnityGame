using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{
   private Inventory inventory;
    //The script to add items into the world, you put location you want the item to spawn and what item you want to spawn
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Gets item correct sprite
    public void SetItem (Item item)
    {
        this.item = item;
        spriteRenderer.sprite = Item.GetSprite(item.itemType);
    }
    //When player picks the item
    public Item GetItem()
    {
        return item;
    }
    //When player picks the item, the item destroys ifself from the ground
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    
}
