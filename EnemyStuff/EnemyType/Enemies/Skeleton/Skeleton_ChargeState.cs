
// @author rasmushy
public class Skeleton_ChargeState : ChargeState
{
    private Skeleton enemy;
    public Skeleton_ChargeState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_ChargeState stateData, Skeleton enemy) : base(etity, stateMachine, animBoolName, stateData)
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
