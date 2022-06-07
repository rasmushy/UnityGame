using UnityEngine;
// @author rasmushy
public class AttackState : State
{
    protected Transform attackPosition;

    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    public AttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(etity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DetectionCheckUp()
    {
        base.DetectionCheckUp();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        entity.animTSM.attackState = this;
        isAnimationFinished = false;
        entity.SetVelocity(0f);
    }

    public virtual void TriggerAttack() // This is here to start damage dealing in right part of the animation, its used in melee or ranged attack states
    {
        
    }

    public virtual void FinishAttack() // Attack is done so animation ends, this is set as event function into animation too
    {
        isAnimationFinished = true; 
    }
}
