using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLookForPlayerStateData", menuName = "Data/State Data/Look For Player State")]

public class Data_LookForPlayerState : ScriptableObject
{
    // turns around to look player for = how many times
    public int turningAmount = 2;
    // seconds
    public float timeBetweenTurns = 0.75f;
}
