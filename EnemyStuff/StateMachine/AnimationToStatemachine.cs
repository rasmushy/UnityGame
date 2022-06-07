using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class AnimationToStatemachine : MonoBehaviour
{
    //This is added to entity's "Alive" gameobject to track when it attacks and dies in animation
    public AttackState attackState;

    public DeadState deadState;
    // so Alive game object directs these to right enemy
    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void FinishedAttack()
    {
        attackState.FinishAttack();
    }

    private void FinishedDeath()
    {
        deadState.FinishDeath();
    }
}
