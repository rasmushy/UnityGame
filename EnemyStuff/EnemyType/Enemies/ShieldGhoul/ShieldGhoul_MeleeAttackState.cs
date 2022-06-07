using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class ShieldGhoul_MeleeAttackState : MeleeAttackState
{
    protected ShieldGhoul enemy;
    

    public ShieldGhoul_MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_MeleeAttackState stateData, ShieldGhoul enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.blockStateData.blocking = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
                stateMachine.ChangeState(enemy.playerDetectedState);
            else
                stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }
}
