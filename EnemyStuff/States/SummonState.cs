
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class SummonState : State
{
    //Summoning state (or Phase 2) for final boss. 
    //We want him to fly up and start spawning units with Enemyspawning script and set gameobject in FinalLevel. 
    private Data_SummonStateData stateData;
    protected bool isSummoningOver;

    public SummonState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Data_SummonStateData stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.rB.gravityScale = -0.001f;
        isSummoningOver = false;
        EnemySpawning enemySpawnerObject = GameObject.FindObjectOfType<EnemySpawning>();
        enemySpawnerObject.StartSpawner(true);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.immortalTime)
        {
            entity.rB.gravityScale = 0.005f;
            if (Time.time >= startTime + stateData.immortalTime * 2)
            {
                isSummoningOver = true;
                stateData.spawnPhaseOver = true;
            }
        }
    }
}
