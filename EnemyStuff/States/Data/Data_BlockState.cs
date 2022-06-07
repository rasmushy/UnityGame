using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBlockStateData", menuName = "Data/State Data/Block State")]

public class Data_BlockState : ScriptableObject
{
    public float blockTime = 1f;
    public float blockCooldown = 2f;
    public bool blocking = false;
}
