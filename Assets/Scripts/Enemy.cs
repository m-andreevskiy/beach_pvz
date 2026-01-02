using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Enemy : MonoBehaviour
{
    public float speed;
    private float SPEED_MULTIPLIER = 0.05F;

    private void Start()
    {

    }


    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x - speed * SPEED_MULTIPLIER * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
