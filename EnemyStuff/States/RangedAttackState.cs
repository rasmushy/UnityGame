using UnityEngine;

public class RangedAttackState : AttackState
{
    // Ranged attack state for archer and final boss
    // It creates projectile when used, that uses projectile.cs script
    protected Data_RangedAttackState stateData;

    protected GameObject projectile;
    protected Projectile projectileScript;

    public RangedAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName,Transform attackPosition, Data_RangedAttackState stateData) : base(etity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(stateData.projectileSpeed,stateData.projectileTravelDistance,stateData.projectileDmg, stateData.deSpawnTimer, stateData.isItSpell);
    }
}
