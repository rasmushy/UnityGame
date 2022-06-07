using UnityEngine;
// @author rasmushy
public class KnockState : State
{
    //KnockState to give additional mechanic when fighting entity's
    protected Data_KnockState stateData;

    protected bool isStunTimeOver;
    protected bool isFloor; // what is ground / floor to detect so we dont fly around the map
    protected bool isMovementStopped;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    public KnockState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_KnockState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DetectionCheckUp()
    {
        base.DetectionCheckUp();
        isFloor = entity.CheckFloor();
        performCloseRangeAction = entity.CheckPlayerInShortRangeAction();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;
        isMovementStopped = false;
        entity.SetVelocityTwo(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, entity.lastDamageDirection); //It uses angle from checkFloor() to detect where to get knocked.
    }

    public override void Exit()
    {
        base.Exit();
        entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.stunKnockbackTime)
            isStunTimeOver = true;

        if (isFloor && Time.time >= startTime + stateData.stunKnockbackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            entity.SetVelocity(0f);
        }
    }
}
