using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class SpearGhoul : Entity
{
    public SpearGhoul_IdleState idleState { get; private set; }
    public SpearGhoul_MoveState moveState { get; private set; }
    public SpearGhoul_PlayerDetectedState playerDetectedState { get; private set; }
    public SpearGhoul_ChargeState chargeState { get; private set; }
    public SpearGhoul_LookForPlayerState lookForPlayerState { get; private set; }
    public SpearGhoul_MeleeAttackState meleeAttackState { get; private set; }
    public SpearGhoul_KnockState knockState { get; private set; }
    public SpearGhoul_DeadState deadState { get; private set; }

    [SerializeField] private Data_IdleState idleStateData;
    [SerializeField] private Data_MoveState moveStateData;
    [SerializeField] private Data_PlayerDetected playerDetectedData;
    [SerializeField] private Data_ChargeState chargeStateData;
    [SerializeField] private Data_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private Data_MeleeAttackState meleeAttackStateData;
    [SerializeField] private Data_KnockState knockStateData;
    [SerializeField] private Data_DeadState deadStateData;
    //melee attack
    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        //// ALL ENEMY STATES STARTS HERE ***********************************************************************************************
        /// Basic states gets created:
        // etity: this, stateMachine, animBoolname: move, stateData[SerializeField], enemy: this
        moveState = new SpearGhoul_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new SpearGhoul_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState =
            new SpearGhoul_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new SpearGhoul_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState =
            new SpearGhoul_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        knockState = new SpearGhoul_KnockState(this, stateMachine, "stunKnock", knockStateData, this);
        deadState = new SpearGhoul_DeadState(this, stateMachine, "dead", deadStateData, this);
        // melee attack
        meleeAttackState = new SpearGhoul_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        //// ALL ENEMY STATES ENDS HERE ***********************************************************************************************
        
        stateMachine.Initialize(moveState);
    }

    // enemy attack radius
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
        Gizmos.DrawWireSphere(gameObject.transform.position, deadStateData.goldGainRadius);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
        if (isDead) // lets check are we dead
        {
            stateMachine.ChangeState(deadState); // put us in deadState
            Destroy(gameObject, deadStateData.deSpawnTimer);
        }
        else if (isStunned && stateMachine.currentState != knockState) // lets check if we are stunned and if not get into knockstate/stunned.
        {
            stateMachine.ChangeState(knockState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }
}
