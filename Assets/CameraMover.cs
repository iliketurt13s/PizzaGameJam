using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform target;
    public float speed;

    void FixedUpdate()
    {
        float newX = Mathf.Lerp(transform.position.x, target.position.x, speed);
        float newY = Mathf.Lerp(transform.position.y, target.position.y, speed);
        transform.position = new Vector3(newX, newY, -10);
    }
}
