using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class Slime_PlayerDetectedState : PlayerDetectedState
{
    private Slime enemy;
    public Slime_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetected stateData, Slime enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performShortRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.chargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (!isDetectingGround)
        {
            entity.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
