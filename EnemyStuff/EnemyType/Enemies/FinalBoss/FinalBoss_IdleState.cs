using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class FinalBoss_IdleState : IdleState
{
    private FinalBoss enemy;

    public FinalBoss_IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_IdleState stateData, FinalBoss enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange)
            stateMachine.ChangeState(enemy.playerDetectedState);
        else if (isIdleTimeOver)
            stateMachine.ChangeState(enemy.moveState);
    }
}
