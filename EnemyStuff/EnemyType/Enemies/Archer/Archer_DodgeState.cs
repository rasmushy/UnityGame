using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class Archer_DodgeState : DodgeState
{
    private Archer enemy;

    public Archer_DodgeState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_DodgeState stateData, Archer enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDodgeOver)
        {
            enemy.SetVelocity(0.0f);
            if (isPlayerInMaxAgroRange && performShortRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if (isPlayerInMaxAgroRange && !performShortRangeAction)
            {
                stateMachine.ChangeState(enemy.rangedAttackState);
            }
            else if (!isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }
}
