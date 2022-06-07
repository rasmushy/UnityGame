using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newHeroknightData", menuName = "Data/Heroknight Data/Base Data")]

public class Data_Heroknight : ScriptableObject
{
    // All Data Heroknight has
    public float playerMaxHealth = 100f;
    public float attackRange = 0.5f;
    public float attackDamage = 30;
    public float attackRadius = 0.5f;
    public float stunDamageAmount = 1f;
    public float movementSpeed = 4.0f;
    public float jumpForce = 7.5f;
    public LayerMask whatIsWall;
    public int curLevel = 0;
    public bool noBlood = false;
    public int goldAmount = 1;
    //Attack
    public LayerMask enemyLayers;
    // Player attack damage testing
    public Vector3 attackPointPosition_R = new Vector3(1, 1, 0);
    public Vector3 attackPointPosition_L = new Vector3(-1, 1, 0);
}