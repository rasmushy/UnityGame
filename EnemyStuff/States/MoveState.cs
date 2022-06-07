public class MoveState : State
{
    //This state is used for moving around (roaming state) 
    protected Data_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingGround;
    protected bool isPlayerInMinAgroRange;

    public MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_MoveState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DetectionCheckUp() // It checks collisions with terrain
    {
        base.DetectionCheckUp();
        isDetectingGround = entity.CheckGround();
        isDetectingWall = entity.CheckWall();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.movementSpeed);
    }
}
