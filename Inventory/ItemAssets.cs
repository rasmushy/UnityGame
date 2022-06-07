using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
   public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    //This is where you put the sprite. Every level has an ItemAssest object, and from there add the sprites for each item
    public Transform pfItemWorld;
    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;
    public Sprite coinSprite;
    public Sprite dmgBSprite;
    public Sprite bookSprite;
}
