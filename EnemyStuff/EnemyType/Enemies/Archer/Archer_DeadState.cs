using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class Archer_DeadState : DeadState
{
    protected Archer enemy;
    public Archer_DeadState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_DeadState stateData, Archer enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(enemy.deadState);
        }
    }
}
