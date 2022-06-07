using UnityEngine;
// @author rasmushy
public class IdleState : State
{
    //Idle state for idling
    protected Data_IdleState stateData;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingWall;
    protected bool isDetectingGround;
    protected float idleTime;

    public IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_IdleState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DetectionCheckUp() //Its also set to detect ledges (isDetectingGround) to make sure we dont go move state while next to ledge
    {
        base.DetectionCheckUp();
        isDetectingGround = entity.CheckGround();
        isDetectingWall = entity.CheckWall();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        isIdleTimeOver = false;
        entity.SetVelocity(0f);
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();
        if (flipAfterIdle) //if its true flip enemy, when we leave idlestate
            entity.Flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + idleTime)
            isIdleTimeOver = true;
    }

    public void SetFlipAfterIdle(bool flip) 
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
