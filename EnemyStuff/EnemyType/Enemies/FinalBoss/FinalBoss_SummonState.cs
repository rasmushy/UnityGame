using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss_SummonState : SummonState
{
    protected FinalBoss enemy;

    public FinalBoss_SummonState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_SummonStateData stateData, FinalBoss enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isSummoningOver)
            stateMachine.ChangeState(enemy.lookForPlayerState);

    }
}
