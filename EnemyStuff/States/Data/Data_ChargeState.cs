using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State Data/Charge State")]
public class Data_ChargeState : ScriptableObject
{
    public float chargeSpeed = 6f;
    // 2seconds of charging
    public float chargeTime = 2f;
}
