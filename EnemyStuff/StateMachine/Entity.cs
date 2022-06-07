using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// @author rasmushy
public class Entity : MonoBehaviour
{
    //This is what every enemy type inherits, it gives all the necessary methods in virtual way so they can be changed as needed. 
    //To create new enemy you need to create all the states for it and data packets for those states
    //Basic states are: IdleState, MoveState,LookForPlayerState,PlayerDetectedState,KnockState and DeadState. 
    //Then depending what you want the entity to do you can add MeleeAttackState, RangedAttackState,DodgeState,BlockState,ChargeState or even SummonState (which is used only by finalboss)
    //Entity based State scripts contains the logic behind how that entity works, so all the states just give them background.

    // We need to have this to be able to change states what we've created
    public FiniteStateMachine stateMachine; 
    
    // ScriptableObject, hold our enemy data. this is for every State.
    public Data_Entity entityData;
    
    public int facingDirection { get; protected set; } //protected set so final boss can use it.
    public Rigidbody2D rB { get; protected set; }
    public Animator anim { get; protected set; }
    public GameObject aliveGO { get; protected set; }
    public GameObject deadGO { get; protected set; }
    public AnimationToStatemachine animTSM { get; protected set; } // script set into Alive GameObject inside of entity

    // Health system for entity
    public HealthSystem entityHealth;

    // knockback/stun animation variable for damaging entity etc
    public int lastDamageDirection { get; protected set; }

    [SerializeField] protected Transform wallCheck; //changed these to protected so finalboss can have its own checks.
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected Transform floorCheck;


    // used to track when you can stun enemy again.
    protected float CurrentStunResistance;
    protected float lastDamageTime;

    private Vector2 velocityVector;
    protected bool isStunned;
    protected bool isDead;
    protected bool isBlocking;


    public virtual void Start()
    {
        isBlocking = false;
        entityHealth = new HealthSystem(entityData.maxHealth); // construct healthsystem for our entity
        //Debug.Log(gameObject.name +" health from Start() = "+entityHealth.GetHealth()); // test it out
        facingDirection = 1; //set our facingDirection to the right
        CurrentStunResistance = entityData.stunKnockResistance;
        deadGO = transform.Find("Dead").gameObject; //We have dead gameobject what we want to use in deadState
        deadGO.gameObject.SetActive(false); // lets deactivate it immediately for now :)
        aliveGO = transform.Find("Alive").gameObject; // We are Alive, this is our gameobject
        rB = aliveGO.GetComponent<Rigidbody2D>(); // get our body
        anim = aliveGO.GetComponent<Animator>(); // and animator
 
        // To State Machine
        animTSM = aliveGO.GetComponent<AnimationToStatemachine>(); // This is needed so we can keep track when we are finishing animations cause main script is not in Alive GameObject. 
        stateMachine = new FiniteStateMachine(); //get out stateMachine rolling so we can start changing our state's
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        anim.SetFloat("yVelocity", rB.velocity.y); // track this to know when dodge animation is over.
        // stun resistance updated if time has passed enough.
        if (Time.time >= lastDamageTime + entityData.stunKnockRecoveryTime)
            ResetStunResistance();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    // So we like to move :), this is our method for that
    public virtual void SetVelocity(float velocity)
    {
        velocityVector.Set(facingDirection * velocity, rB.velocity.y);
        rB.velocity = velocityVector;
    }
    // new version, it actually works with same name :D, this can be used in knockbacking or making enemy dodge etc.
    public virtual void SetVelocityTwo(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityVector.Set(angle.x * velocity * direction, angle.y * velocity);
        rB.velocity = velocityVector;
    }

    public virtual bool CheckWall() // We dont want to hit walls.
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance,
            entityData.whatIsGround);
    }
    
    public virtual bool CheckGround() // this might aswell be renamed into CheckLedge(), but is it too late?
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, entityData.groundCheckDistance,
            entityData.whatIsGround);
    }
    
    public virtual bool CheckFloor() // We need to check floor to get proper angle in knockbacks
    {
        return Physics2D.OverlapCircle(floorCheck.position, entityData.floorCheckRadius, entityData.whatIsGround);
    }

    public virtual bool CheckPlayerInMinAgroRange() //Player checking 
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAgroDistance,
            entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange() // Where did he go?
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance,
            entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInShortRangeAction() //If hes close enough do shortRangeAction (melee)
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.shortRangeActionDistance,
            entityData.whatIsPlayer);
    }

    public virtual void Flip() // lets flip our sprite, this one works in rotation to get our gizmos set up nicely, might change later to flipX sprite?
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f,180f,0);
    }

    public virtual void DamageKnock(float velocity) // Knockback when you get damaged
    {
        velocityVector.Set(rB.velocity.x,velocity);
        rB.velocity = velocityVector;
    }

    public virtual void ResetStunResistance() // Function to reset our stun resistance
    {
        isStunned = false;
        CurrentStunResistance = entityData.stunKnockResistance;
    }

    public virtual void Damage(AttackDetails attackDetails) // Damage function to TAKE damage
    {
        if (attackDetails.position.x > aliveGO.transform.position.x) //just to know what direction we can be stunned
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if (!isBlocking)
        {
            entityHealth.Damage(attackDetails.damageAmount); // deal damage to our healthsystem
            DamageKnock(entityData.damageKnockSpeed); //knockback us like we have been set up. 
            lastDamageTime = Time.time; //calculate last time we got damaged
            if (entityHealth.GetHealthPercent() <= 0.0f) // are we dead?
            {
                Debug.Log(gameObject.name + " Died");
                isDead = true;
                aliveGO.gameObject.layer = 10; // mark it away from enemy layer so it doesnt take damage anymore.
            }
            else
            {
                CurrentStunResistance -= attackDetails.stunDamageAmount; //check if we can get stunned aswell
            }

            if (CurrentStunResistance <= 0) //are we stunned?
                isStunned = true;
        }
        else if(isBlocking && facingDirection == lastDamageDirection)
        {
            Debug.Log(gameObject.name+" blocked your attack :D");
        }
    }

    // This is to setup enemy in game, every single gizmos can be configured from Data files attached to the entity
    public virtual void OnDrawGizmos()
    {
        // drawing a line to wallcheck position to right, if we are facing right -> the distance of our wallcheckdistance
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + (Vector3)(Vector2.down * entityData.groundCheckDistance));
        //Attack sphere
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.shortRangeActionDistance), 0.2f);
        //Agro spheres
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAgroDistance), 0.4f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.maxAgroDistance), 0.4f);
        //floor check
        Gizmos.DrawWireSphere(floorCheck.position + (Vector3)(Vector2.right * facingDirection), entityData.floorCheckRadius);
    }

}
