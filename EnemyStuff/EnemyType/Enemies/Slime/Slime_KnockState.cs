using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class Slime_KnockState : KnockState
{
    private Slime enemy;

    public Slime_KnockState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_KnockState stateData, Slime enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isStunTimeOver)
        {// Decide what state we going after stun time is over
            if (performCloseRangeAction)
                stateMachine.ChangeState(enemy.meleeAttackState);
            else if (isPlayerInMaxAgroRange)
                stateMachine.ChangeState(enemy.chargeState);
            else if (!isPlayerInMaxAgroRange)
            {
                enemy.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }
}
