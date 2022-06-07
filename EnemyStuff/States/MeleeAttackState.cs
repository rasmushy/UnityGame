using UnityEngine;

public class MeleeAttackState : AttackState
{
    //Melee attack state checks attackposition transform and delivers message to Heroknight TakeDamage()
    protected Data_MeleeAttackState stateData;
    // using struct to get details so dont have to update everywhere.
    protected AttackDetails attackDetails;


    public MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_MeleeAttackState stateData) : base(etity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = entity.aliveGO.transform.position;
    }

    public override void TriggerAttack() // Melee attack
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects =
            Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        attackDetails.damageAmount = stateData.attackDamage; // get stuff from our Data and add it to the attackDetail struct
        attackDetails.position = entity.aliveGO.transform.position;

        foreach (Collider2D player in detectedObjects)
        {
            player.transform.GetComponent<HeroKnight>().TakeDamage(attackDetails);
        }
    }
}
