using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameEndMenu gameEndMenu;
    [SerializeField] private GameObject[] spawnLines;

    [SerializeField] private TMP_Text sandAmountText;
    [SerializeField] private TMP_Text clayAmountText;
    [SerializeField] private TMP_Text pearlsAmountText;
    [SerializeField] private ResourceStorage resourceStorage;

    public GameObject draggingObject;
    public GameObject currentContainer;
    private int newCastleLine;
    private PauseMenu pauseMenu;
    public bool gameGoesOn = true;
    public bool allEnemiesSpawned = false;

    public GameObject globalCastle = null;
    public List<GameObject>[] enemies = new List<GameObject>[3];
    public GameObject testEnemy;
    public Camera mainCamera;
    public static GameManager instance;
    List<GameObject> castles = new List<GameObject>();
    private int sandAmount = 100;
    private int clayAmount= 100;
    private int pearlsAmount = 10;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pauseMenu = GetComponent<PauseMenu>();

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = new List<GameObject>();
        }

    }

    private void Update()
    {
        sandAmountText.text = sandAmount.ToString();
        clayAmountText.text = clayAmount.ToString();
        pearlsAmountText.text = pearlsAmount.ToString();


        if (gameGoesOn && allEnemiesSpawned)
        {
            enemies[0].RemoveAll(item => item == null);
            enemies[1].RemoveAll(item => item == null);
            enemies[2].RemoveAll(item => item == null);

            if (enemies[0].Count == 0 
                && enemies[1].Count == 0 
                && enemies[2].Count == 0
            )
            {
                EndGame(true);
            }

        }
    }


    public void AddResource(string resource, int amount)
    {
        // Debug.Log($"Add resource is called ({resource}, {amount}");
        switch (resource)
        {
            case "sand":
                sandAmount += amount;
                break;

            case "clay":
                clayAmount += amount;
                break;

            case "pearls":
                pearlsAmount += amount;
                break;

            default:
                break;
        }
    }

    public void SpawnEnemy(GameObject enemy, int line)
    {
        enemies[line].Add(enemy);
        enemy.transform.position = spawnLines[line].transform.position + new UnityEngine.Vector3(0, 0, -1);
    }


    public void PlaceObject(ObjectDrag objectDrag)
    {
        if (draggingObject != null && currentContainer != null)
        {
            CastleBase newCastleBase = objectDrag.GetPrefab().GetComponent<CastleBase>();
            int sandCost = newCastleBase.getCostInSand();
            int clayCost = newCastleBase.getCostInClay();
            int pearlsCost = newCastleBase.getCostInPearls();

            if (sandCost > sandAmount)
            {
                resourceStorage.LackHighlight();
                print("not enough sand >.<");
                return;
            }
            if (clayCost > clayAmount)
            {
                resourceStorage.LackHighlight();
                print("not enough clay >.<");
                return;
            }
            if (pearlsCost > pearlsAmount)
            {
                resourceStorage.LackHighlight();
                print("not enough pearls >.<");
                return;
            }

            sandAmount -= sandCost;
            clayAmount -= clayCost;
            pearlsAmount -= pearlsCost;
            

            ObjectContainer newCastleContainer = currentContainer.GetComponent<ObjectContainer>();
            currentContainer.GetComponent<ObjectContainer>().isFull = true;
            newCastleLine = currentContainer.GetComponent<ObjectContainer>().line;

            UnityEngine.Vector3 position = currentContainer.transform.position;

            position = mainCamera.ScreenToWorldPoint(position);
            position.z = 1; // a bit farther from camera to let enemies be over castles


            if (objectDrag.GetPrefab().GetComponent<CastleBase>().isChild)
            {
                // Debug.Log("this is child");
                globalCastle = Instantiate(objectDrag.GetPrefab(), position, UnityEngine.Quaternion.identity);
                globalCastle.GetComponent<ChildBase>().gameManager = this;
                globalCastle.GetComponent<CastleBase>().assignedContainer = newCastleContainer;
                PlaceObjectContinue();
                return;
            }



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

            GameObject.Find("castleBuilder").GetComponent<CastleBuilder>().Init(position, newCastleLine, objectDrag, newCastleContainer);

        }
    }
    public void PlaceObjectContinue()
    {
        //Instantiate(draggingObject.GetComponent<ObjectDrag>().card.object_Game, currentContainer.transform);
        castles.Add(globalCastle);
        globalCastle.GetComponent<CastleBase>().isBuilt = true;
    }


    public void EndGame(bool isWin)
    {
        gameEndMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;
        gameGoesOn = false;

        if (isWin)
        {
            gameEndMenu.ShowWin();
        }
        else
        {
            gameEndMenu.ShowLose();
        }
    }

    public void RestartGame()
    {
        enemySpawner.Restart();
        gameGoesOn = true;
        allEnemiesSpawned = false;

        Time.timeScale = 1f;
        gameEndMenu.gameObject.SetActive(false);

        foreach(var line in enemies)
        {
            foreach(var enemy in line)
            {
                Destroy(enemy);
            }
        }

        foreach(var castle in castles)
        {
            Destroy(castle);
        }
    }

    public bool IsGamePaused()
    {
        if (pauseMenu != null)
        {
            return pauseMenu.IsPaused();
        }
        else
        {
            print("No PauseMenu attached to gameObject of GameManager");
            return false;
        }
    }
}
