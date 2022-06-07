using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class SpearGhoul_DeadState : DeadState
{
    protected SpearGhoul enemy;

    public SpearGhoul_DeadState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_DeadState stateData, SpearGhoul enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DetectionCheckUp()
    {
        base.DetectionCheckUp();
    }

    public override void DropGold()
    {
        base.DropGold();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishDeath()
    {
        base.FinishDeath();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(enemy.deadState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
