using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnLines;
    public GameObject draggingObject;
    public GameObject currentContainer;
    private int newCastleLine;

    // public CastleSerialized globalCastle = new CastleSerialized();
    public GameObject globalCastle = null;
    public List<GameObject>[] enemies = new List<GameObject>[3];
    public GameObject testEnemy;
    public Camera mainCamera;
    public static GameManager instance;

    List<GameObject> castles = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = new List<GameObject>();
        }

        // enemies[1].Add(testEnemy);
    }

    public void SpawnEnemy(GameObject enemy, int line)
    {
        enemies[line].Add(enemy);
        enemy.transform.position = spawnLines[line].transform.position;
    }

    // public void KillEnemy(GameObject enemy)
    // {
    //     for (int i = 0; i < enemies.Length; i++)
    //     {
    //         enemies[i].find(enemy);
    //     }

    // }

    public void PlaceObject()
    {
        if (draggingObject != null && currentContainer != null)
        {
            currentContainer.GetComponent<ObjectContainer>().isFull = true;
            newCastleLine = currentContainer.GetComponent<ObjectContainer>().line;

            // Instantiate(draggingObject.GetComponent<ObjectDrag>().card.object_Game, currentContainer.transform);

            UnityEngine.Vector3 position = currentContainer.transform.position;
            // print("old pos = " + position);
            // print("old screen-to-world pos" + Camera.main.ScreenToWorldPoint(position));

            // position = currentContainer.transform.InverseTransformPoint(transform.position);
            // position = mainCamera.WorldToViewportPoint(position);
            position = mainCamera.ScreenToWorldPoint(position);
            position.z = -5;
            // print("new pos = " + position);

            foreach (GameObject gameObject in SceneManager.GetSceneByName("MainGame").GetRootGameObjects())
            {
                if (gameObject.name != "GameManager")
                {
                    gameObject.SetActive(false);
                    if (gameObject.name == "MiniGame")
                    {
                        gameObject.SetActive(true);
                    }
                }
            }
            //foreach (GameObject gameObject in SceneManager.GetSceneByName("CastleBuilding").GetRootGameObjects())
            //{
            //    gameObject.SetActive(true);
            //}

            GameObject.Find("castleBuilder").GetComponent<CastleBuilder>().Init(position, newCastleLine);
            // GameObject.Find("castleBuilder").GetComponent<CastleBuilder>().Init(new Vector2());

        }
    }
    public void PlaceObjectContinue()
    {
        //Instantiate(draggingObject.GetComponent<ObjectDrag>().card.object_Game, currentContainer.transform);
        castles.Add(globalCastle);
        globalCastle.GetComponent<Castle>().isBuilt = true;
    }
}
