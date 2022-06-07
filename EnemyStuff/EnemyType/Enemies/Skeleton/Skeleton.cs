
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Android;
// @author rasmushy
public class Skeleton : Entity
{
    public Skeleton_IdleState idleState { get; private set; }
    public Skeleton_MoveState moveState { get; private set; }
    public Skeleton_PlayerDetectedState playerDetectedState { get; private set; }
    public Skeleton_ChargeState chargeState { get; private set; }
    public Skeleton_LookForPlayerState lookForPlayerState { get; private set; }
    public Skeleton_MeleeAttackState meleeAttackState { get; private set; }
    public Skeleton_KnockState knockState { get; private set; }
    public Skeleton_DeadState deadState { get; private set; }


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
        moveState = new Skeleton_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Skeleton_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState =
            new Skeleton_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new Skeleton_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState =
            new Skeleton_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        knockState = new Skeleton_KnockState(this, stateMachine, "stunKnock", knockStateData, this);
        deadState = new Skeleton_DeadState(this, stateMachine, "dead", deadStateData, this);
        // melee attack
        meleeAttackState = new Skeleton_MeleeAttackState(this, stateMachine, "meleeAttack",  meleeAttackPosition, meleeAttackStateData, this);
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
        else if (!CheckPlayerInMinAgroRange()) //if we take dmg from behind we want to turnaround
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }

}
