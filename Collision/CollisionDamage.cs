using UnityEngine;
// @author rasmushy
public class CollisionDamage : MonoBehaviour, ICollisionHandler
{
    private AttackDetails attackDetails;
    // Start is called before the first frame update
    void Start()
    {
        attackDetails.damageAmount = 1000;
        attackDetails.position = transform.position;
        attackDetails.stunDamageAmount = 0;
    }

    public void CollisionEnter(string colliderName, GameObject other)
    {
        if (colliderName == "DamageArea" && other.tag == "Player")
        {
            other.GetComponent<HeroKnight>().TakeDamage(attackDetails);
        }
    }
}
