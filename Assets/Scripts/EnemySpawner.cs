using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] private int enemiesFinalNumber = 6;
  [SerializeField] private GameManager gameManager;
  [SerializeField] private GameObject enemyPrefab;
  [SerializeField] private float spawnRate = 2;

  private float BASE_SPAWN_TIME = 10;
  private float spawnTimer = 0;
  private int line;
  private int enemiesSpawnedCount = 0;


  public void Restart()
  {
    enemiesSpawnedCount = 0;
    spawnTimer = 0;
  }

  private void Start()
  {

  }


  private void FixedUpdate()
  {
    if (enemiesSpawnedCount >= enemiesFinalNumber)
    {
      return;
    }

    spawnTimer += Time.deltaTime;

    if (spawnTimer >= BASE_SPAWN_TIME / spawnRate)
    {
      spawnTimer = 0;
      GameObject newEnemy = Instantiate(enemyPrefab, this.transform);
      line = Random.Range(0, 3);
      // line = 0;
      gameManager.SpawnEnemy(newEnemy, line);

      enemiesSpawnedCount++;
      if (enemiesSpawnedCount >= enemiesFinalNumber)
      {
        gameManager.allEnemiesSpawned = true;
      }
    }

  }
}
