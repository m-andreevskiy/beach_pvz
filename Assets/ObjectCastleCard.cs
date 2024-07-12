using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectCastleCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject object_Drag;
    public GameObject object_Game;
    public Canvas canvas;
    private GameObject object_Drag_Instance;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }
    public void OnDrag(PointerEventData eventData)
    {
        object_Drag_Instance.transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        object_Drag_Instance = Instantiate(object_Drag, canvas.transform);
        object_Drag_Instance.transform.position = Input.mousePosition;
        object_Drag_Instance.GetComponent<ObjectDrag>().card = this;
        gameManager.draggingObject = object_Drag_Instance;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameManager.PlaceObject();
        gameManager.draggingObject = null;
        Destroy(object_Drag_Instance);
    }
}
