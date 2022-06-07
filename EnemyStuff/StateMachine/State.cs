using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class State
{
    // This is what we give to every state we build (attackstate,movestate etc..)
    // Point is to have logic where we go into state and get off the state
    // Use protected so inherited classes gets it


    //This whole statemachine idea is created by Bardent. Changes are minimal (protected instead of private etc.)
    protected FiniteStateMachine stateMachine;
    protected Entity entity;

    public float startTime { get; protected set;} //get & private set allows us to get time from other states

    protected string animBoolName;

    public State(Entity etity, FiniteStateMachine stateMachine,string animBoolName)
    {
        this.entity = etity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        // lets store time for every state
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }
    
    public virtual void PhysicsUpdate()
    {
        DetectionCheckUp(); // this is for checking walls agro what ever you want
    }

    public virtual void DetectionCheckUp()
    {

    }


}
