
// @author rasmushy
public class Skeleton_LookForPlayerState : LookForPlayerState
{
    private Skeleton enemy;


    public Skeleton_LookForPlayerState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_LookForPlayerState stateData, Skeleton enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        // to make sure we are not permanently in look for player
        isTurningDone = false;
        isTurningTimeDone = false;
        lastTurnTime = startTime;
        amountOfTurnsDone = 0;
        entity.SetVelocity(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // if enemy finds player it goes back to detection state, if not then moving state
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }else if (isTurningTimeDone)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
