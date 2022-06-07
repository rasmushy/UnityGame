using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public float zOffet = 0f;
    public Transform target;

    //This class makes the camera follow the target. In this instance it follows the player.
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        //Gets target (players) x and y position, and follows it. The -20f is the Z axel
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -20f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
