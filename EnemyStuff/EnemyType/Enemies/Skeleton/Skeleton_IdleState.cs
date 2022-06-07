
// @author rasmushy
public class Skeleton_IdleState : IdleState
{
    private Skeleton enemy;
    public Skeleton_IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName,Data_IdleState stateData, Skeleton enemy) : base(etity, stateMachine, animBoolName, stateData)
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
