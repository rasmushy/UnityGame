using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State")]

public class Data_RangedAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDmg = 10f;
    public float projectileSpeed = 12f;
    public float projectileTravelDistance = 5f;
    public float deSpawnTimer = 5f;
    public bool isItSpell = false;
}
