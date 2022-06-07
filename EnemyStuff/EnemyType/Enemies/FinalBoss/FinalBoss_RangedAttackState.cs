using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class FinalBoss_RangedAttackState : RangedAttackState
{
    private FinalBoss enemy;

    public FinalBoss_RangedAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_RangedAttackState stateData, FinalBoss enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
                stateMachine.ChangeState(enemy.playerDetectedState);
            else if(!isPlayerInMaxAgroRange)
                stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }
}
