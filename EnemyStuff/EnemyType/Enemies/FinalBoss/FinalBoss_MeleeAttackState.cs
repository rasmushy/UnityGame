using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class FinalBoss_MeleeAttackState : MeleeAttackState
{
    protected FinalBoss enemy;

    public FinalBoss_MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_MeleeAttackState stateData, FinalBoss enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //When melee attack animation is doned we go back to player detected or if we dont see him then look for him
        if (isAnimationFinished)
        { 
            if (isPlayerInMinAgroRange)
                stateMachine.ChangeState(enemy.playerDetectedState);
            else if(!isPlayerInMaxAgroRange)
                stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }
}
