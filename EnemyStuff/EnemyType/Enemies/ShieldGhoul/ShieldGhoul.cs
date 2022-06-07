using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGhoul : Entity
{

    public ShieldGhoul_IdleState idleState { get; private set; }
    public ShieldGhoul_MoveState moveState { get; private set; }
    public ShieldGhoul_PlayerDetectedState playerDetectedState { get; private set; }
    public ShieldGhoul_BlockState blockState { get; private set; }
    public ShieldGhoul_LookForPlayerState lookForPlayerState { get; private set; }
    public ShieldGhoul_MeleeAttackState meleeAttackState { get; private set; }
    public ShieldGhoul_KnockState knockState { get; private set; }
    public ShieldGhoul_DeadState deadState { get; private set; }

    [SerializeField] private Data_IdleState idleStateData;
    [SerializeField] private Data_MoveState moveStateData;
    [SerializeField] private Data_PlayerDetected playerDetectedData;
    [SerializeField] private Data_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private Data_MeleeAttackState meleeAttackStateData;
    [SerializeField] private Data_KnockState knockStateData;
    [SerializeField] private Data_DeadState deadStateData;
    [SerializeField] public Data_BlockState blockStateData;
    //melee attack
    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        //// ALL ENEMY STATES STARTS HERE ***********************************************************************************************
        /// Basic states gets created: 
        // etity: this, stateMachine, animBoolname: move, stateData[SerializeField], enemy: this
        moveState = new ShieldGhoul_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new ShieldGhoul_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState =
            new ShieldGhoul_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        blockState = new ShieldGhoul_BlockState(this, stateMachine, "block", blockStateData, this);
        lookForPlayerState =
            new ShieldGhoul_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        knockState = new ShieldGhoul_KnockState(this, stateMachine, "stunKnock", knockStateData, this);
        deadState = new ShieldGhoul_DeadState(this, stateMachine, "dead", deadStateData, this);
        // melee attack
        meleeAttackState = new ShieldGhoul_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
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
        if (blockStateData.blocking)
        {
            isBlocking = true;
        }
        else
        {
            isBlocking = false;
        }
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
