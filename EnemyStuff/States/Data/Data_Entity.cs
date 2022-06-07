using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class Data_Entity : ScriptableObject
{
    
    public float maxHealth = 100f;
    //knockback speed
    public float damageKnockSpeed = 3f;
    // floor checking for velocity knockbacks/dodging etc, check entity VelocityTwo() 
    public float floorCheckRadius = 0.3f;
    // Stun resistance & recovery time
    public float stunKnockResistance = 3f;
    public float stunKnockRecoveryTime = 2f;
    public float wallCheckDistance = 0.2f;
    public float groundCheckDistance = 0.4f;

    // aggro distances
    public float minAgroDistance = 1f;
    public float maxAgroDistance = 4f;
    // attack range for melee attack
    public float shortRangeActionDistance = 1f;

    //public GameObject hitParticle;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
