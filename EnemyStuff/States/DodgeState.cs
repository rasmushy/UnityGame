using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class DodgeState : State
{
    //Dodge state for archer, he jumps now.
    protected Data_DodgeState stateData;

    protected bool performShortRangeAction;
    protected bool isPlayerInMaxAgroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;


    public DodgeState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_DodgeState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DetectionCheckUp()
    {
        base.DetectionCheckUp();
        performShortRangeAction = entity.CheckPlayerInShortRangeAction();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isGrounded = entity.CheckFloor();
    }

    public override void Enter()
    {
        base.Enter();
        isDodgeOver = false;
        //SetVelocityTwo can be used as dodge / knockback
        entity.SetVelocityTwo(stateData.dodgeSpeed, stateData.dodgeAngle, -entity.facingDirection); // jump to opposite direction we are facing

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.dodgeTime && isGrounded) //check if dodging is over
            isDodgeOver = true;
    }
}
