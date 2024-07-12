using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class fallingLetter : MonoBehaviour
{
    public float VELOCITY = -5;
    // public float baseSpawnHeight = 10;
    // public GameObject receiver;
    SpriteRenderer sr;

    Rigidbody2D rb;
    Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        // tr.position = new Vector3(receiver.GetComponent<Transform>().position.x, tr.position.y, 0);

        // sr = GetComponent<SpriteRenderer>();
        // sr.sprite = receiver.GetComponent<SpriteRenderer>().sprite;

    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity = new Vector2(0, VELOCITY);
        tr.position += new Vector3(0, Time.deltaTime*VELOCITY, 0);

    }
}
