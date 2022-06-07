using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class FiniteStateMachine
{
    // this is what the entity's own script will be changing with statemachine.ChangeState() method
    //This whole statemachine idea is created by Bardent. Changes are minimal (protected instead of private etc.)
    public State currentState { get; private set; }

    public void Initialize(State startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }



}
