using UnityEngine;
// @author rasmushy
public class Projectile : MonoBehaviour
{

    //Idea behind this code is mostly from Bardent's tutorial, some additional logic added like "is it a spell" and other small changes to make it work in this game.
    private AttackDetails attackDetails;
    
    private float speed;
    private float verticalStartPos;
    private float travelDistance;
    private float deSpawnTimer;

    [SerializeField] private float gravity;
    [SerializeField] private float damageRadius;

    private bool isGravityOn;
    private bool hasHitGround; // use this to turn off the projectile
    private bool isItSpell;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform damagePosition;

    private Rigidbody2D rB;


    private void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        rB.gravityScale = 0.0f;
        rB.velocity = transform.right * speed;

        isGravityOn = false;

        verticalStartPos = transform.position.x;
        Destroy(gameObject, deSpawnTimer);
    }

    private void Update()
    {
        if (!hasHitGround)
        {
            attackDetails.position = transform.position;

            if (isGravityOn)
            {
                float angle = Mathf.Atan2(rB.velocity.y, rB.velocity.x) *Mathf.Rad2Deg; // Use Mathf Atan2 to get angle and turn it from radians to Degrees
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        if (isItSpell && hasHitGround) //if its a spell it destroys itself automatically when colliding.
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if (damageHit)
            {
                damageHit.GetComponent<HeroKnight>().TakeDamage(attackDetails);
                Destroy(gameObject); // destroy arrow
            }

            if (groundHit)
            {
                hasHitGround = true;
                rB.gravityScale = 0f;
                rB.velocity = Vector2.zero;
            }


            if (Mathf.Abs(verticalStartPos - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rB.gravityScale = gravity;
            }
        }
    }

    public void FireProjectile(float speed, float travelDistance, float damage, float deSpawnTimer, bool isItSpell)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        this.deSpawnTimer = deSpawnTimer;
        this.isItSpell = isItSpell;
        attackDetails.damageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
