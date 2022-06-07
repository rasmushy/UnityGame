using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/State Data/Dead State")]
public class Data_DeadState : ScriptableObject
{
    public float goldGainRadius = 10f;
    public int minGoldDrop = 20;
    public int maxGoldDrop = 30;
    public LayerMask whatIsPlayer;
    // seconds
    public float deSpawnTimer = 5f;
}
