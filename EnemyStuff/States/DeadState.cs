using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class DeadState : State
{
    //When entity dies it needs to drop gold (or give exp) and start playing animations etc.

    protected Data_DeadState stateData;
    protected Vector2 deadPosition;
    
    protected bool isAnimationFinished;
    protected bool areWeFinalBoss;
    protected int goldDropAmount;

    public DeadState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_DeadState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        areWeFinalBoss = false;
        entity.animTSM.deadState = this;
        isAnimationFinished = false;
        entity.SetVelocity(0f);
        // lets get our DeadGameObject into same place where our Death animation played and flip our body if needed.
        deadPosition = entity.aliveGO.transform.position;
        entity.deadGO.gameObject.transform.position = deadPosition;
        if (entity.lastDamageDirection > 0)
        {
            entity.deadGO.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
        entity.aliveGO.gameObject.SetActive(false);
        entity.deadGO.gameObject.SetActive(true); //this gameobject is just dead corpse of the enemy
        DropGold();
    }

    // Death is done so animation ends
    public virtual void FinishDeath()
    {
        isAnimationFinished = true;

    }

    // random gold amount from set min & max values
    private void SetRandomGoldAmount()
    {
        goldDropAmount = Random.Range(stateData.minGoldDrop, stateData.maxGoldDrop);
    }

    //Drop gold, but also make sure are we finalboss, if we are we give this detail to heroknight(playerscript)
    public virtual void DropGold()
    {
        SetRandomGoldAmount();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(deadPosition, stateData.goldGainRadius, stateData.whatIsPlayer);
        foreach (Collider2D player in hitEnemies)
            player.transform.GetComponent<HeroKnight>().DropEntityGold(goldDropAmount, areWeFinalBoss);
        Debug.Log("We want to drop this amount to player: " + goldDropAmount);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DetectionCheckUp()
    {
        base.DetectionCheckUp();
    }
}