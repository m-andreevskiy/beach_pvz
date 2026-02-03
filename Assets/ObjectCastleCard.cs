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
            object_Drag_Instance.transform.position = Input.mousePosition;
        }
        else
        {
            gameManager.draggingObject = null;
            Destroy(object_Drag_Instance);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        object_Drag_Instance = Instantiate(object_Drag.gameObject, canvas.transform);
        object_Drag_Instance.transform.position = Input.mousePosition;
        // object_Drag_Instance.GetComponent<ObjectDrag>().card = this;
        gameManager.draggingObject = object_Drag_Instance;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameManager.PlaceObject(object_Drag);
        gameManager.draggingObject = null;
        Destroy(object_Drag_Instance);
    }
}
