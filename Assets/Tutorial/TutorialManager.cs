using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] private GameObject[] popups;
    [SerializeField] private float[] popupsWaitTime;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private GameObject pearlSpawner;
    [SerializeField] private int stateWithAccessToBuildCastle_1 = 6;

    private int currentState = 0;
    private float waitTime = 3;
    private bool keepCounting = true;


    public bool CanBuildCastle_1()
    {
        return currentState >= stateWithAccessToBuildCastle_1;
    }

    void Start()
    {
        waitTime = popupsWaitTime[0];
        enemySpawner.SetActive(false);
        pearlSpawner.SetActive(false);
    }


    void Update()
    {
        if (currentState >= popupsWaitTime.Length)
        {
            /** Tutorial ended */
            return;
        }

        if (keepCounting)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                switch (currentState)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        popups[currentState].SetActive(true);
                        keepCounting = false;
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

                if (currentState == 4)
                {
                    pearlSpawner.SetActive(true);
                }


                popups[currentState].SetActive(false);
                currentState++;
                if (currentState < popupsWaitTime.Length)
                {
                    keepCounting = true;
                    waitTime = popupsWaitTime[currentState];
                }

            }
        }




    }
}
