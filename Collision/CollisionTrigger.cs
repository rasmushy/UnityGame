using UnityEngine;
// @author rasmushy
public class CollisionTrigger : MonoBehaviour
{
    private ICollisionHandler handler;
    //collision trigger to detect when player gets close to collision to take dmg etc..
    private void Start()
    {
        handler = GetComponentInParent<ICollisionHandler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // here it calls handler that detects collision
        handler.CollisionEnter(gameObject.name, collision.gameObject);
    }
}
