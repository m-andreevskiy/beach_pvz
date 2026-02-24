using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectCastleCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public ObjectDrag object_Drag;

    [SerializeField] private TMP_Text sandCostText;
    [SerializeField] private TMP_Text clayCostText;
    [SerializeField] private TMP_Text pearlsCostText;

    public bool isFirstCastle = false;
    public Canvas canvas;
    private GameObject object_Drag_Instance;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        
        GameObject castlePrefab = object_Drag.GetPrefab();
        CastleBase castleBase = castlePrefab.GetComponent<CastleBase>();
        sandCostText.text = castleBase.getCostInSand().ToString();
        clayCostText.text = castleBase.getCostInClay().ToString();
        pearlsCostText.text = castleBase.getCostInPearls().ToString();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!gameManager.IsGamePaused())
        {
            // object_Drag_Instance.transform.position = Input.mousePosition;
            object_Drag_Instance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1);
        }
        else
        {
            gameManager.draggingObject = null;
            Destroy(object_Drag_Instance);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // object_Drag_Instance = Instantiate(object_Drag.gameObject, canvas.transform);
        object_Drag_Instance = Instantiate(object_Drag.gameObject);
        // object_Drag_Instance.transform.position = Input.mousePosition;
        object_Drag_Instance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1);
        gameManager.draggingObject = object_Drag_Instance;

        // object_Drag_Instance.GetComponent<ObjectDrag>().card = this;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isFirstCastle)
        {
            // Dirty way to correctly guide palyer through the tutorial
            return;
        }

        gameManager.PlaceObject(object_Drag);
        gameManager.draggingObject = null;
        Destroy(object_Drag_Instance);
    }
}
