using UnityEngine;

public class PlayerDetectedState : State
{
    //We need state that we cycle in everytime we have been doing actions (melee attack, ranged attack etc.) 
    //This state is for that reason
    protected Data_PlayerDetected stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    // every enemy has longrange action and shortrange action (melee & range logic)
    protected bool performLongRangeAction;
    protected bool performShortRangeAction;
    //detection
    protected bool isDetectingGround;

    public PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetected stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    
    public override void DetectionCheckUp() //It makes all the checks so we know what we want to do
    {
        base.DetectionCheckUp();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isDetectingGround = entity.CheckGround();
        performShortRangeAction = entity.CheckPlayerInShortRangeAction();
    }

    public override void Enter()
    {
        base.Enter();
        performLongRangeAction = false;
        entity.SetVelocity(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.longRangeActionTime)
            performLongRangeAction = true;
        
    }
}
