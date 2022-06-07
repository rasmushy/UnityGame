using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class FinalBoss_DeadState : DeadState
{
    protected FinalBoss enemy;

    public FinalBoss_DeadState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_DeadState stateData, FinalBoss enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DropGold()
    {
        areWeFinalBoss = true;
        base.DropGold();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
            stateMachine.ChangeState(enemy.deadState);
    }
}
