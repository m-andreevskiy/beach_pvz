using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] private TutorialPopupBase[] popups;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private GameObject pearlSpawner;
    [SerializeField] private GameObject deadline;
    [SerializeField] private GameObject UIResources;
    [SerializeField] private GameObject UICastle1;
    [SerializeField] private GameObject UICastle2;
    [SerializeField] private GameObject UIChild;



    private int currentState = 0;
    private float waitTime = 3;
    private bool keepCounting = true;
    private ObjectCastleCard scriptCastle1;


    public bool isComplete = false;
    



    void Start()
    {
        waitTime = popups[0].GetWaitTime();
        scriptCastle1 = UICastle1.GetComponent<ObjectCastleCard>();
        scriptCastle1.isFirstCastle = true;

        enemySpawner.SetActive(false);
        pearlSpawner.SetActive(false);
        deadline.SetActive(false);
        UIResources.SetActive(false);
        UICastle1.SetActive(false);
        UICastle2.SetActive(false);
        UIChild.SetActive(false);
    }


    void Update()
    {
        if (currentState >= popups.Length)
        {
            /** Tutorial ended */
            return;
        }

        if (keepCounting)
        {
            waitTime -= Time.unscaledDeltaTime;
            if (waitTime <= 0)
            {
                popups[currentState].gameObject.SetActive(true);
                keepCounting = false;

                switch (currentState)
                {

                    case 4:
                        Time.timeScale = 0.2f;
                        break;

                    case 7:
                        UICastle1.SetActive(true);
                        deadline.SetActive(true);
                        break;

                    case 8:
                        scriptCastle1.enabled = false;
                        break;
                    


                    default:
                        keepCounting = false;
                        break;
                }
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
            {
                if (currentState == 3)
                {
                    enemySpawner.SetActive(true);
                }


                /** Start building castle */
                if (currentState == 13)
                {
                    scriptCastle1.isFirstCastle = false;

                    scriptCastle1.enabled = true;
                    scriptCastle1.OnPointerUp(null);
                }

                // if (currentState == 4)
                // {
                //     pearlSpawner.SetActive(true);
                // }


                popups[currentState].Disappear();
                currentState++;
                if (currentState < popups.Length)
                {
                    keepCounting = true;
                    waitTime = popups[currentState].GetWaitTime();
                }

            }
        }




    }
}
