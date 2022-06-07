public class Skeleton_MoveState : MoveState
{
    private Skeleton enemy; 
// @author rasmushy
    public Skeleton_MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_MoveState stateData, Skeleton enemy) : base(etity, stateMachine, animBoolName, stateData)
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
