using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class Archer_MeleeAttackState : MeleeAttackState
{
    protected Archer enemy;

    public Archer_MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_MeleeAttackState stateData, Archer enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isPlayerInMaxAgroRange)
                stateMachine.ChangeState(enemy.playerDetectedState);
            else if(!isPlayerInMaxAgroRange)
                stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }
}
