using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class ChargeState : State
{
    //For melee entity's we can use charge state to catch up our player (or make final boss to run away from player) 

    protected Data_ChargeState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingGround;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performShortRangeAction;

    public ChargeState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_ChargeState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DetectionCheckUp()
    {
        base.DetectionCheckUp();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isDetectingGround = entity.CheckGround();
        isDetectingWall = entity.CheckWall();
        performShortRangeAction = entity.CheckPlayerInShortRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        entity.SetVelocity(stateData.chargeSpeed);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.chargeTime) // if time is larger than starttime + set chargetime then charging is over.
        {
            isChargeTimeOver = true;
        }
    }
}
