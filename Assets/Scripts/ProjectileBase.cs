using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class ProjectileBase : MonoBehaviour
{
    public float VELOCITY = 5;
    [SerializeField] private int damage;

    Transform tr;
    private Health hitHealth = null;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        tr.position += new Vector3(Time.deltaTime * VELOCITY, 0, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("colliding with " + collision.gameObject.name);
        // print("is this an enemy? - " + collision.CompareTag("Enemy"));

        if (collision.CompareTag("Enemy")) {
            hitHealth = collision.GetComponent<Health>();
            if (hitHealth != null) {
                hitHealth.Damage(damage);
                Destroy(gameObject);
            }
            else {
                print("no 'Health' component");
            }
        }

    }
    // public void OnTriggerExit2D(Collider2D collision)
    // {
        // print("trigger exiting");
    // }

}
