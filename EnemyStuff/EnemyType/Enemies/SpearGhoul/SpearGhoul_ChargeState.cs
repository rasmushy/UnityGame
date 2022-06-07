using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class SpearGhoul_ChargeState : ChargeState
{
    private SpearGhoul enemy;

    public SpearGhoul_ChargeState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_ChargeState stateData, SpearGhoul enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Attack if we get in range of short range action else ->
        if (performShortRangeAction)
            stateMachine.ChangeState(enemy.meleeAttackState);
        else if (!isDetectingGround || isDetectingWall) // ground needs to be false so when there is no ground it detects ledge
            stateMachine.ChangeState(enemy.lookForPlayerState);
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
                stateMachine.ChangeState(enemy.playerDetectedState);
            else if(!isPlayerInMinAgroRange)
                stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }
}
