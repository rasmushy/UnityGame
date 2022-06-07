using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class Archer_PlayerDetectedState : PlayerDetectedState
{
    private Archer enemy;
    public Archer_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetected stateData, Archer enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performShortRangeAction)
        {
            if (Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCooldown)
                stateMachine.ChangeState(enemy.dodgeState);
            else
                stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.rangedAttackState);
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
