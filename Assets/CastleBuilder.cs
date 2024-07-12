using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
// using System.Numerics;
using Unity.Collections;
using UnityEditor;
// using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class CastleSerialized
{
    public int health;
    public int damage;
    public List<Vector2> positions;
    public List<Quaternion> rotations;
    public List<Sprite> sprites;

    public CastleSerialized(int hp, int dam, List<Vector2> pos, List<Quaternion> rot, List<Sprite> sp){
        health = hp;
        damage = dam;
        positions = pos;
        rotations = rot;
        sprites = sp;
    }

    public CastleSerialized(){
        positions = new List<Vector2>();
        rotations = new List<Quaternion>();
        sprites = new List<Sprite>();
    }
}



public class CastleBuilder : MonoBehaviour
{

    // public CastleSerialized globalCastle = new CastleSerialized();

    public GameObject brickPrefab;
    public GameObject letterPrefab;
    public LetterReceiver receiver_0;
    public LetterReceiver receiver_1;
    public LetterReceiver receiver_2;
    public LetterReceiver receiver_3;

    public LetterReceiver[] letterReceivers;

    public GameManager gameManager;
    public GameObject castlePrefab;

    // List<Vector3>[] fallingLetters = new List<Vector3>[4];
    List<GameObject>[] fallingLetters = new List<GameObject>[4];

    public float DDRLettersSpawnHeightOffset = 3;
    public float DDRLettersBaseSpawnHeight = 10;


    AudioManager audioManager;


    Rigidbody2D rb;
    SpriteRenderer sr;
    // public Vector3 BlockLaunchingPosition = new Vector3(-5, 0, -1);
    public Vector3 BlockLaunchingPosition = new Vector3(-7, -1.3f, -1);

    // float INITIAL_VELOCITY_Y = 11;
    float INITIAL_VELOCITY_Y = 9;
    float BLOCK_SIZE = 0.64f;
    float FALLING_LETTER_SIZE = 1.28f;
    float BLOCK_MASS_DEFAULT = 1000;
    // float BLOCK_MASS_LAYER_CHANGE = 100;
    float BLOCK_MASS_LAYER_MULTIPLIER = 0.1f;
    // public float brickPresicion = 0.15f;
    float TOTAL_INACCURACY = 0;
    int CASTLE_MAX_HEALTH = 100;
    int CASTLE_MAX_DAMAGE = 100;

    UnityEngine.Vector2 wind = new UnityEngine.Vector2(0, 0);
    // Vector2 WIND_ON_AUTO_LAUNCH = new Vector2(5, 5);

    List<BrickToLaunch> bricksToLaunch = new List<BrickToLaunch>();



    public void Init(Vector3 position)
    {
        gameManager.globalCastle = Instantiate(castlePrefab, position, Quaternion.identity);
        // print(position);
        gameManager.globalCastle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);


        for (int i = 0; i < fallingLetters.Length; i++){
            fallingLetters[i] = new List<GameObject>();
        }
        
        PrepareCastle();
        SpawnDDRLetters();
    }


    // Start is called before the first frame update
    void Start()
    {
        //Init();
    }

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < fallingLetters.Length; i++){
            if (fallingLetters[i].Count > 0){
                if (fallingLetters[i].First<GameObject>().transform.position.y - letterReceivers[i].transform.position.y < -FALLING_LETTER_SIZE)
                {
                    // print("before wind in autolaunching, receiver " + i);
                    wind = letterReceivers[i].calculateWind(fallingLetters[i].First<GameObject>());
                    LaunchPreparedBrick(wind, i);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && fallingLetters[0].Count > 0){
            wind = receiver_0.calculateWind(fallingLetters[0].First<GameObject>());
            // print(wind);
            LaunchPreparedBrick(wind, receiver_0.receiverID);
        }


        if (Input.GetKeyDown(KeyCode.W) && fallingLetters[1].Count > 0){
            wind = receiver_1.calculateWind(fallingLetters[1].First<GameObject>());
            // print(wind);
            LaunchPreparedBrick(wind, receiver_1.receiverID);
        }


        if (Input.GetKeyDown(KeyCode.E) && fallingLetters[2].Count > 0){
            wind = receiver_2.calculateWind(fallingLetters[2].First<GameObject>());
            // print(wind);
            LaunchPreparedBrick(wind, receiver_2.receiverID);
        }


        if (Input.GetKeyDown(KeyCode.R) && fallingLetters[3].Count > 0){
            wind = receiver_3.calculateWind(fallingLetters[3].First<GameObject>());
            // print(wind);
            LaunchPreparedBrick(wind, receiver_3.receiverID);
        }


    }



    void LaunchPreparedBrick(UnityEngine.Vector2 wind, int receiverID)
    {
        StartCoroutine(LaunchBrick(bricksToLaunch.First<BrickToLaunch>(), wind));

        // print("brick pos 4 = " + GameObject.FindGameObjectWithTag("Brick").transform.localPosition);
        GameObject letterToDelete = fallingLetters[receiverID].First<GameObject>();
        fallingLetters[receiverID].RemoveAt(0);
        Destroy(letterToDelete);
        bricksToLaunch.RemoveAt(0);

        // print("wind = " + wind);
        TOTAL_INACCURACY += Math.Abs(wind.x) + Math.Abs(wind.y);
        // print("inaccuracy = " + TOTAL_INACCURACY);
    }


    class BrickToLaunch
    {
        public int gridX;
        public int gridY;
        public string spriteName;
        public float mass;

        public BrickToLaunch(int x, int y, string spriteName, float m)
        {
            gridX = x;
            gridY = y;
            this.spriteName = spriteName;
            mass = m;
        }
    }



    void PrepareCastle()
    {
        TextAsset castleFile = Resources.Load("CastleConfigs/castle_1") as TextAsset;

        string castleText = castleFile.text;
        string[] fLines = castleText.Split("\r\n");
        string[] blocks;
        float blockMass = BLOCK_MASS_DEFAULT;

        for ( int i = fLines.Length - 1; i >= 0; i-- ) {
            
            blocks = fLines[i].Split(";");

            for (int j = blocks.Length - 1; j >= 0; j--){
                switch (blocks[j])
                {
                    case "clay":
                    case "sand":
                    case "canon":
                        bricksToLaunch.Add(new BrickToLaunch(j, fLines.Length - 1 - i, blocks[j], blockMass));
                        
                        break;

                    case "-":
                        break;

                    default:
                        break;
                }
            }

            blockMass *= BLOCK_MASS_LAYER_MULTIPLIER;
        }


    }


    void SpawnDDRLetters() 
    {
        float spawnHeight = DDRLettersBaseSpawnHeight;
        int receiverNumber;
        Sprite letterSprite = receiver_0.GetComponent<SpriteRenderer>().sprite;

        Vector3 letterSpawnPos = new Vector3(0, 0, 0);
        for (int i = 0; i < bricksToLaunch.Count; i++)
        {
            receiverNumber = UnityEngine.Random.Range(0, 4);
            // receiverNumber = UnityEngine.Random.Range(1, 1);
            switch (receiverNumber)
            {
                case 0:
                    letterSpawnPos.x = receiver_0.GetComponent<Transform>().position.x;
                    letterSprite = receiver_0.GetComponent<SpriteRenderer>().sprite;
                    break;

                case 1:
                    letterSpawnPos.x = receiver_1.GetComponent<Transform>().position.x;
                    letterSprite = receiver_1.GetComponent<SpriteRenderer>().sprite;
                    break;

                case 2:
                    letterSpawnPos.x = receiver_2.GetComponent<Transform>().position.x;
                    letterSprite = receiver_2.GetComponent<SpriteRenderer>().sprite;
                    break;

                case 3:
                    letterSpawnPos.x = receiver_3.GetComponent<Transform>().position.x;
                    letterSprite = receiver_3.GetComponent<SpriteRenderer>().sprite;
                    break;
                
                default:
                    break;

            }
            letterSpawnPos.y = spawnHeight;

            GameObject newLetter = Instantiate(letterPrefab, letterSpawnPos, Quaternion.identity);
            newLetter.GetComponent<SpriteRenderer>().sprite = letterSprite;
            fallingLetters[receiverNumber].Add(newLetter);

            spawnHeight += DDRLettersSpawnHeightOffset;
        }

    }



    
   
    // public CastleSerialized serializeCastle()
    public void serializeCastle()
    {
        print("serialization");
        print("total inaccuracy = " + TOTAL_INACCURACY);
        // gllobalCastle.free
        GameObject gCastle = gameManager.globalCastle;
        // CastleSerialized castle = new CastleSerialized();
        gCastle.GetComponent<Castle>().health = Math.Max(5, CASTLE_MAX_HEALTH - (int)(TOTAL_INACCURACY*10));
        gCastle.GetComponent<Castle>().damage = Math.Max(5, CASTLE_MAX_DAMAGE - (int)(TOTAL_INACCURACY*10));

        print("Casrle health = " + gCastle.GetComponent<Castle>().health);

        foreach (var brick in GameObject.FindGameObjectsWithTag("Brick"))
        {
            brick.transform.SetParent(gameManager.globalCastle.GetComponent<Transform>(), false);
            // print("brick pos ser = " + brick.transform.localPosition);
            brick.GetComponent<BoxCollider2D>().enabled = false;
            brick.GetComponent<Rigidbody2D>().simulated = false;
            // brick.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            // gCastle.positions.Add(brick.GetComponent<Transform>().position);
            // gCastle.rotations.Add(brick.GetComponent<Transform>().rotation);
            // gCastle.sprites.Add(brick.GetComponent<SpriteRenderer>().sprite);

            // GameObject.Destroy(brick);
        }


        // SceneManager.LoadScene("Main");
        //foreach (GameObject gameObject in SceneManager.GetSceneByName("CastleBuilding").GetRootGameObjects())
        //{
        //    print(gameObject.name);
        //    if (gameObject.name != "castle"){
        //        gameObject.SetActive(false);
        //    }

        //}
        //// GameObject.Find("castle").SetActive(true);
        //foreach (GameObject gameObject in SceneManager.GetSceneByName("MainGame").GetRootGameObjects())
        //{
        //    gameObject.SetActive(true);
        //}
        foreach (GameObject gameObject in SceneManager.GetSceneByName("MainGame").GetRootGameObjects())
        {
                gameObject.SetActive(true);
                if (gameObject.name == "MiniGame")
                {
                    gameObject.SetActive(false);
                }
        }

        GameObject.Find("GameManager").GetComponent<GameManager>().globalCastle = gCastle;
        GameObject.Find("GameManager").GetComponent<GameManager>().PlaceObjectContinue();

    }



    private IEnumerator LaunchBrick(BrickToLaunch brick, UnityEngine.Vector2 wind)
    {
        // print("L pos = " + BlockLaunchingPosition);
        GameObject newBrick = Instantiate(brickPrefab, BlockLaunchingPosition, Quaternion.identity);
        // print("brick pos 0 = " + newBrick.transform.localPosition);
        newBrick.GetComponent<Rigidbody2D>().simulated = false;
        // print("brick pos 1 = " + newBrick.transform.localPosition);

        
        // print("brick pos 2 = " + newBrick.transform.localPosition);

        
        rb = newBrick.GetComponent<Rigidbody2D>();
        sr = newBrick.GetComponent<SpriteRenderer>();

        switch (brick.spriteName)
        {
            case "clay":
                sr.sprite = Resources.Load<Sprite>("CastleBlocks/clay64");
                break;

            case "sand":
                sr.sprite = Resources.Load<Sprite>("CastleBlocks/sand");
                break;

            case "canon":
                sr.sprite = Resources.Load<Sprite>("CastleBlocks/gun");
                break;
        }

        if (bricksToLaunch.Count <= 1){
            print("invoking serialization");
            Invoke("serializeCastle", 3);
        }

        audioManager.PlayBrickLaunchSound();
        yield return new WaitForSeconds(0.2f);
        rb.velocity = ComputeInitialSpeed(brick.gridX, brick.gridY) + wind;
        rb.mass = brick.mass;
        rb.simulated = true;

        // print("brick pos 3 = " + newBrick.transform.localPosition);
        
    }

    Vector2 ComputeInitialSpeed(int gridX, int gridY)
    {
        float offsetX = BLOCK_SIZE * 6;

        float x = offsetX + gridX * BLOCK_SIZE;
        // float x = destination.x - BlockLaunchingPosition.x;
        float y = gridY * BLOCK_SIZE;
        // float y = destination.y - BlockLaunchingPosition.y;

        float g = Physics2D.gravity.y;


        // float initialVelocityX = x/(2*y) * (INITIAL_VELOCITY_Y - Mathf.Sqrt(INITIAL_VELOCITY_Y*INITIAL_VELOCITY_Y - 2*g*y));
        float initialVelocityX = x*g / (-INITIAL_VELOCITY_Y - Mathf.Sqrt(INITIAL_VELOCITY_Y*INITIAL_VELOCITY_Y + 2*g*y));
        // INITIAL_VELOCITY_Y += 0.5f;
        return new Vector2(initialVelocityX, INITIAL_VELOCITY_Y);
    }
}
