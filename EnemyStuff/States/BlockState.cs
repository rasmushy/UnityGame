using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class BlockState : State
{

    //Block state for ShieldGhoul, he can block now.

    protected Data_BlockState stateData;

    protected bool isBlockingOver;
    protected bool performShortRangeAction;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    public BlockState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_BlockState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
     
    public override void DetectionCheckUp()
    {
        base.DetectionCheckUp();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        performShortRangeAction = entity.CheckPlayerInShortRangeAction();
    }

    public override void Enter()
    {
        base.Enter();
        isBlockingOver = false;
        stateData.blocking = true;
        entity.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.blockTime) // if time is larger than starttime + set blocktime then blocking is over.
        {
            isBlockingOver = true;
            stateData.blocking = false; // set our entity's blocking data back to false.
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
