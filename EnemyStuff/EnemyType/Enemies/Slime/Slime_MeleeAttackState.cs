using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class Slime_MeleeAttackState : MeleeAttackState
{
    protected Slime enemy;

    public Slime_MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_MeleeAttackState stateData, Slime enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {//When melee attack animation is doned we go back to player detected or if we dont see him then look for him
            if (isPlayerInMinAgroRange)
                stateMachine.ChangeState(enemy.playerDetectedState);
            else if (!isPlayerInMaxAgroRange)
                stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }
}
