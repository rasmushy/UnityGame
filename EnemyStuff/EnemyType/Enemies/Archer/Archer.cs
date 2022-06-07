using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Archer : Entity
{

    public Archer_IdleState idleState { get; private set; }
    public Archer_MoveState moveState { get; private set; }
    public Archer_PlayerDetectedState playerDetectedState { get; private set; }
    public Archer_LookForPlayerState lookForPlayerState { get; private set; }
    public Archer_MeleeAttackState meleeAttackState { get; private set; }
    public Archer_KnockState knockState { get; private set; }
    public Archer_DeadState deadState { get; private set; }
    public Archer_DodgeState dodgeState { get; private set; }
    public Archer_RangedAttackState rangedAttackState { get; private set; }

    [SerializeField] private Data_IdleState idleStateData;
    [SerializeField] private Data_MoveState moveStateData;
    [SerializeField] private Data_PlayerDetected playerDetectedData;
    [SerializeField] private Data_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private Data_MeleeAttackState meleeAttackStateData;
    [SerializeField] private Data_KnockState knockStateData;
    [SerializeField] private Data_DeadState deadStateData;
    [SerializeField] private Data_RangedAttackState rangedAttackStateData;
    [SerializeField] public Data_DodgeState dodgeStateData;

    //melee attack
    [SerializeField] private Transform meleeAttackPosition;
    //ranged attack
    [SerializeField] private Transform rangedAttackPosition;

    public override void Start()
    {
        base.Start();

        //// ALL ENEMY STATES STARTS HERE ***********************************************************************************************
        /// Basic states gets created:
        // etity: this, stateMachine, animBoolname: move, stateData[SerializeField], enemy: this
        moveState = new Archer_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Archer_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState =
            new Archer_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        lookForPlayerState =
            new Archer_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        knockState = new Archer_KnockState(this, stateMachine, "stunKnock", knockStateData, this);
        deadState = new Archer_DeadState(this, stateMachine, "dead", deadStateData, this);
        dodgeState = new Archer_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);
        // melee attack
        meleeAttackState = new Archer_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        // ranged attack
        rangedAttackState = new Archer_RangedAttackState(this, stateMachine, "rangedAttack",rangedAttackPosition, rangedAttackStateData, this);
        //// ALL ENEMY STATES ENDS HERE ***********************************************************************************************
        
        stateMachine.Initialize(moveState);
    }

    // enemy attack radius
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
        Gizmos.DrawWireSphere(rangedAttackPosition.position, 0.4f);
        Gizmos.DrawWireSphere(gameObject.transform.position, deadStateData.goldGainRadius);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails); //we override the damage but we still want to use entity's damage function so lets call it with base()
        if (isDead) // lets check are we dead
        {
            stateMachine.ChangeState(deadState); // put us in deadState
            Destroy(gameObject, deadStateData.deSpawnTimer);
        }
        else if (isStunned && stateMachine.currentState != knockState) // lets check if we are stunned and if not get into knockstate/stunned.
        {
            stateMachine.ChangeState(knockState);
        }
        else if (CheckPlayerInMinAgroRange())
        {
            stateMachine.ChangeState(rangedAttackState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }
}
