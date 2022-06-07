using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class ShieldGhoul_KnockState : KnockState
{
    private ShieldGhoul enemy;

    public ShieldGhoul_KnockState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_KnockState stateData, ShieldGhoul enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if (isPlayerInMinAgroRange && Time.time >= startTime + enemy.blockStateData.blockCooldown)
            {
                stateMachine.ChangeState(enemy.blockState);
            }
            else if(!isPlayerInMinAgroRange)
            {
                enemy.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }
}
