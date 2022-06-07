using UnityEngine;
// @author rasmushy
public class LookForPlayerState : State
{

    //State when we lost sight on player, can be used to flip around entity faster
    protected Data_LookForPlayerState stateData;

    protected bool turnImmediately;
    protected bool isPlayerInMinAgroRange;
    protected bool isTurningDone;
    protected bool isTurningTimeDone;
    protected float lastTurnTime;
    protected int amountOfTurnsDone;

    public LookForPlayerState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_LookForPlayerState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DetectionCheckUp()
    {
        base.DetectionCheckUp();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (turnImmediately)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            turnImmediately = false;
        }else if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isTurningDone)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        if (amountOfTurnsDone >= stateData.turningAmount)
            isTurningDone = true;

        if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && isTurningDone)
            isTurningTimeDone = true;


    }

    // to look around asap, we can flip instantly with this function
    public void SetTurnImmediately(bool flip)
    {
        turnImmediately = flip;
    }
}
