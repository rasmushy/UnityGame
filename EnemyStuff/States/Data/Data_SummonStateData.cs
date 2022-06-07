using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newSummonStateData", menuName = "Data/State Data/Summon State")]

public class Data_SummonStateData : ScriptableObject
{
    public float immortalTime = 15f;
    public float gravityScaleData = 1f;
    public bool spawnPhaseOver = false;
}
