using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class Archer_IdleState : IdleState
{
    private Archer enemy;
    public Archer_IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_IdleState stateData, Archer enemy) : base(etity, stateMachine, animBoolName, stateData)
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
