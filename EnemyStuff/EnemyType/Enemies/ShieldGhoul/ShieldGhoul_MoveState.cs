using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class ShieldGhoul_MoveState : MoveState
{
    private ShieldGhoul enemy;

    public ShieldGhoul_MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_MoveState stateData, ShieldGhoul enemy) : base(etity, stateMachine, animBoolName, stateData)
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
        else if (isDetectingWall || !isDetectingGround) //Detecting ground is false if there is ledge.
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
