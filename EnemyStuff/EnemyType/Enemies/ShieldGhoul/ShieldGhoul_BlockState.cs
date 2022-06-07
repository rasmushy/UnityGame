using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class ShieldGhoul_BlockState : BlockState
{
    private ShieldGhoul enemy;

    public ShieldGhoul_BlockState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_BlockState stateData, ShieldGhoul enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isBlockingOver)
        {
            if (isPlayerInMinAgroRange && performShortRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if (!isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }
}
