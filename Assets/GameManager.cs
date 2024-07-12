using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject draggingObject;
    public GameObject currentContainer;
    // public CastleSerialized globalCastle = new CastleSerialized();
    public GameObject globalCastle = null;
    public Camera mainCamera;
    public static GameManager instance;

    List<GameObject> castles = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }
    
    public void PlaceObject()
    {
        if (draggingObject != null && currentContainer != null)
        {
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

            // print(currentContainer.transform.position);
            GameObject.Find("castleBuilder").GetComponent<CastleBuilder>().Init((position));
            // GameObject.Find("castleBuilder").GetComponent<CastleBuilder>().Init(new Vector2());

            // print("Hello");

            
        }
    }
public void PlaceObjectContinue()
    {

        //Instantiate(draggingObject.GetComponent<ObjectDrag>().card.object_Game, currentContainer.transform);
        castles.Add(globalCastle);
        
    }
}
