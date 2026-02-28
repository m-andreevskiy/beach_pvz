using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{

    // [SerializeField] private TutorialPopupBase[] popups;
    [SerializeField] private GameObject popupsObject;
    [SerializeField] private GameManager gameManagerScript;
    [SerializeField] private CastleBuilder CastleBuilderScript;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private GameObject pearlSpawner;
    [SerializeField] private GameObject deadline;
    [SerializeField] private GameObject UIResources;
    [SerializeField] private GameObject UICastle1;
    [SerializeField] private GameObject UICastle2;
    [SerializeField] private GameObject UIChild;

    public float slowedTimeScale_1 = 0.2f;
    public float slowedTimeScale_2 = 0.01f;



    private TutorialPopupBase[] popupList;
    private int currentState = 0;
    private float waitTime = 3;
    private bool keepCounting = true;
    private ObjectCastleCard castleCardScript1;
    private ObjectCastleCard childCardScript;
    private GameObject currentContainer;
    private GameObject draggingObject;


    public bool isPaused = false;
    public bool isComplete = false;
    public static TutorialManager instance;
    


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        /** Fill in popup list */
        popupList = new TutorialPopupBase[popupsObject.transform.childCount];
        int childIndex = 0;
        foreach (Transform t in popupsObject.transform)
        {
            popupList[childIndex] = t.gameObject.GetComponent<TutorialPopupBase>();
            childIndex++;
        }
        

        waitTime = popupList[0].GetWaitTime();
        castleCardScript1 = UICastle1.GetComponent<ObjectCastleCard>();
        castleCardScript1.isFirstCastle = true;
        childCardScript = UIChild.GetComponent<ObjectCastleCard>();
        childCardScript.isFirstCastle = true;

        enemySpawner.SetActive(false);
        pearlSpawner.SetActive(false);
        pearlSpawner.GetComponent<PearlSpawner>().enabled = false;
        deadline.SetActive(false);
        UIResources.SetActive(false);
        UICastle1.SetActive(false);
        UICastle2.SetActive(false);
        UIChild.SetActive(false);
    }


    void Update()
    {
        if (gameManagerScript.IsGamePaused() || isPaused)
        {
            return;
        }

        if (currentState >= popupList.Length)
        {
            /** Tutorial ended */
            return;
        }

        /** No popup is displayed and we are waiting for the next one */
        if (keepCounting)
        {
            waitTime -= Time.unscaledDeltaTime;
            if (waitTime <= 0)
            {
                popupList[currentState].gameObject.SetActive(true);
                keepCounting = false;

                /** Some specific actions for certain stages */
                switch (currentState)
                {

                    /** Slow down enemies */
                    case 4:
                        Time.timeScale = slowedTimeScale_1;
                        break;

                    /** Able to place first castle */
                    case 7:
                        UICastle1.SetActive(true);
                        deadline.SetActive(true);
                        break;

                    /** Prevent placing another castle while still showing next popups*/
                    case 8:
                        castleCardScript1.enabled = false;
                        break;
                    
                    case 14:
                        /** Slow down falling letters */
                        Time.timeScale = slowedTimeScale_2;
                        break;

                    case 23:
                        UIChild.SetActive(true);
                        childCardScript.isFirstCastle = true;
                        UIResources.SetActive(true);
                        break;

                    case 25:
                        pearlSpawner.SetActive(true);
                        pearlSpawner.GetComponent<PearlSpawner>().enabled = true;
                        break;


                    default:
                        keepCounting = false;
                        break;
                }
            }
        }

        /** Some popup is displayed and we are waiting for the player action */
        else
        {            
            if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
            {
                if (currentState == 3)
                {
                    enemySpawner.SetActive(true);
                }

                if (currentState == 7)
                {
                    if (gameManagerScript.draggingObject == null 
                    || gameManagerScript.currentContainer == null
                    || gameManagerScript.currentContainer.GetComponent<ObjectContainer>().backgroundImage.enabled == false
                    )
                    {
                        /** Don't show next popup if player hasn't dragged castle over container */
                        return;
                    }

                    currentContainer = gameManagerScript.currentContainer;
                    draggingObject = gameManagerScript.draggingObject;
                }

                /** Start building castle */
                if (currentState == 13)
                {
                    CastleBuilderScript.receiversAreEnabled = false;

                    gameManagerScript.currentContainer = currentContainer;
                    gameManagerScript.draggingObject = draggingObject;

                    castleCardScript1.isFirstCastle = false;

                    castleCardScript1.OnPointerUp(null);
                    
                }

                if (currentState == 18)
                {
                    /** Let the player be the guitar hero */
                    CastleBuilderScript.receiversAreEnabled = true;
                    Time.timeScale = 0.5f;

                    /** Wait until the castle is built */
                    isPaused = true;
                }

                if (currentState == 23)
                {
                    if (gameManagerScript.draggingObject == null 
                    || gameManagerScript.currentContainer == null
                    || gameManagerScript.currentContainer.GetComponent<ObjectContainer>().backgroundImage.enabled == false
                    )
                    {
                        /** Don't show next popup if player hasn't dragged child over container */
                        return;
                    }

                    childCardScript.isFirstCastle = false;
                    childCardScript.OnPointerUp(null);
                }

                if (currentState == 26)
                {
                    castleCardScript1.enabled = true;
                    UICastle2.SetActive(true);
                    Time.timeScale = 1;
                    isComplete = true;
                }


                CloseCurrentPopup();
                
            }
        

            /** Special case for castle building process*/
            if (currentState == 14)
            {
            if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.R))
                {
                    CloseCurrentPopup();
                }
                
            }

        }


    }

    public void CloseCurrentPopup()
    {
        popupList[currentState].Disappear();
        currentState++;
        if (currentState < popupList.Length)
        {
            keepCounting = true;
            waitTime = popupList[currentState].GetWaitTime();
        }
    }
}
