using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGhoul_PlayerDetectedState : PlayerDetectedState
{
    private ShieldGhoul enemy;
    public ShieldGhoul_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetected stateData, ShieldGhoul enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performShortRangeAction)
        {
            if (Time.time >= enemy.blockState.startTime + enemy.blockStateData.blockCooldown)
            {
                stateMachine.ChangeState(enemy.blockState);
            }
            else if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.blockState);
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
