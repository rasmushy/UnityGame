using UnityEngine;
// @author rasmushy
public interface ICollisionHandler
{
    // Collision handler interface for damage etc.
    void CollisionEnter(string colliderName, GameObject other);
}


