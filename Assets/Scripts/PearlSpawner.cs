using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class PearlSpawner : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private GameObject pearlPrefab;
    [SerializeField] private float spawnRate = 10;

    private float BASE_SPAWN_TIME = 20;
    private float spawnTimer = 0;
    private Vector3 spawnPos = new(0, 0, 0);

    private new BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        spawnPos.z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= BASE_SPAWN_TIME / spawnRate)
        {
            spawnTimer = 0;

            spawnPos.x = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
            spawnPos.y = Random.Range(collider.bounds.min.y, collider.bounds.max.y);

            GameObject newPearl = Instantiate(pearlPrefab, spawnPos, Quaternion.identity);
            newPearl.GetComponent<PearlBase>().gameManager = gameManager;
            
            gameManager.SpawnPearl(newPearl);
        }

    }
}
