using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  public GameManager gameManager;
  public GameObject enemyPrefab;
  public float spawnRate = 2;

  private float BASE_SPAWN_TIME = 10;

  private float spawnTimer = 0;
  private int line;

  private void Start()
  {

  }


  private void FixedUpdate()
  {
    spawnTimer += Time.deltaTime;

    if (spawnTimer >= BASE_SPAWN_TIME / spawnRate)
    {
      spawnTimer = 0;
      GameObject newEnemy = Instantiate(enemyPrefab, this.transform);
      line = Random.Range(0, 3);
      // print("spawning enemy on line " + line);
      gameManager.SpawnEnemy(newEnemy, line);
    }

  }
}
