using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Entity
{
    public FinalBoss_IdleState idleState { get; private set; }
    public FinalBoss_MoveState moveState { get; private set; }
    public FinalBoss_PlayerDetectedState playerDetectedState { get; private set; }
    public FinalBoss_LookForPlayerState lookForPlayerState { get; private set; }
    public FinalBoss_MeleeAttackState meleeAttackState { get; private set; }
    public FinalBoss_KnockState knockState { get; private set; }
    public FinalBoss_DeadState deadState { get; private set; }
    public FinalBoss_SummonState summonState { get; private set; }
    public FinalBoss_RangedAttackState rangedAttackState { get; private set; }


    public HealthBar bossHealthBar; //healthbar for boss

    [SerializeField] private Data_IdleState idleStateData;
    [SerializeField] private Data_MoveState moveStateData;
    [SerializeField] private Data_PlayerDetected playerDetectedData;
    [SerializeField] private Data_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private Data_MeleeAttackState meleeAttackStateData;
    [SerializeField] private Data_KnockState knockStateData;
    [SerializeField] private Data_DeadState deadStateData;
    [SerializeField] private Data_RangedAttackState rangedAttackStateData;
    [SerializeField] private Data_SummonStateData summonStateData;
    //[SerializeField] public Data_DodgeState dodgeStateData;

    //melee attack
    [SerializeField] private Transform meleeAttackPosition;
    //ranged attack
    [SerializeField] private Transform rangedAttackPosition;

    public override void Start()
    {
        base.Start();
        summonStateData.spawnPhaseOver = false;
        bossHealthBar.Setup(entityHealth);
        facingDirection = -1; //sprite is left sided

        //// ALL ENEMY STATES STARTS HERE ***********************************************************************************************
        /// Basic states gets created:
        // etity: this, stateMachine, animBoolname: move, stateData[SerializeField], enemy: this
        moveState = new FinalBoss_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new FinalBoss_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState =
            new FinalBoss_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        lookForPlayerState =
            new FinalBoss_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        knockState = new FinalBoss_KnockState(this, stateMachine, "stunKnock", knockStateData, this);
        deadState = new FinalBoss_DeadState(this, stateMachine, "dead", deadStateData, this);

        // melee attack
        meleeAttackState = new FinalBoss_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        // ranged attack
        rangedAttackState = new FinalBoss_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);
        // Summoning phase (FINAL BOSS STATE)
        summonState = new FinalBoss_SummonState(this, stateMachine, "summonState", summonStateData, this);
        //// ALL ENEMY STATES ENDS HERE ***********************************************************************************************
        
        stateMachine.Initialize(moveState);
    }

    // enemy attack radius
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
        Gizmos.DrawWireSphere(rangedAttackPosition.position, 0.2f); // draw attackposition for ranged attack
        Gizmos.DrawWireSphere(gameObject.transform.position, deadStateData.goldGainRadius);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
        
        if (entityHealth.GetHealthPercent() <= 0.5f && !summonStateData.spawnPhaseOver)
        {
            SetVelocity(0f);
            isBlocking = true;
            stateMachine.ChangeState(summonState);
        }else if (isDead) // lets check are we dead
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
    // override cause animation sprite is pointing left, direction changed.
    public override bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right *-1, entityData.wallCheckDistance,
            entityData.whatIsGround);
    }

    public override bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right *-1, entityData.minAgroDistance,
            entityData.whatIsPlayer);
    }

    public override bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right *-1, entityData.maxAgroDistance,
             entityData.whatIsPlayer);
    }

    public override bool CheckPlayerInShortRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right *-1, entityData.shortRangeActionDistance,
            entityData.whatIsPlayer);
    }

    public override void Update()
    {
        base.Update();

        
        if (summonStateData.spawnPhaseOver)
        {
            isBlocking = false;
            rB.gravityScale = summonStateData.gravityScaleData;
        }
    }

}
