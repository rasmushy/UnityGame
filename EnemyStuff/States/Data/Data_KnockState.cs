
using UnityEngine;

[CreateAssetMenu(fileName = "newKnockStateData", menuName = "Data/State Data/Knock State")]
public class Data_KnockState : ScriptableObject
{
    // seconds
    public float knockTime = 3f;

    public float stunKnockbackTime = 0.2f;
    public float stunKnockbackSpeed = 20f;
    public Vector2 stunKnockbackAngle;

}
