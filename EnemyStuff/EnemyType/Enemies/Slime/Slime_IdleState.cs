using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class Slime_IdleState : IdleState
{
    private Slime enemy;
    public Slime_IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_IdleState stateData, Slime enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
