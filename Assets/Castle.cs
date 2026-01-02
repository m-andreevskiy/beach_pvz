using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{

    public LayerMask enemyMask;
    public GameManager gameManager;
    public GameObject projectilePrefab;
    public float health;
    public float damage;
    public float attackSpeed = 10;
    public Vector2 position;
    public int line;
    public bool isBuilt = false;

    private float attackTimer = 0;
    private float baseAttackTime = 10;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;

        if (gameManager != null)
        {
            if (isBuilt && attackTimer >= baseAttackTime / attackSpeed)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1000, enemyMask);
                if (hit)
                {
                    // print ("seeing " + hit.collider.gameObject.name);
                    attackTimer = 0;
                    Shoot();
                }
                else {
                    // print ("hit is null");
                }
            }
        }
    }

    private void Shoot()
    {
        // print("shooting on line " + line);
        Instantiate(projectilePrefab, this.transform);

    }
}
