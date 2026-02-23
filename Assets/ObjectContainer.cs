using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectContainer : MonoBehaviour
{
    public bool isFull;
    private GameManager gameManager;
    public SpriteRenderer backgroundImage;
    public Image backgroundImageCanvas;
    public int line;

    private void Start()
    {
        gameManager = GameManager.instance;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameManager.draggingObject != null && isFull == false)
        {
            gameManager.currentContainer = this.gameObject;
            backgroundImage.enabled = true;
        }


    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        backgroundImage.enabled = false;

        if (isFull) 
        {
            return;
        }
        
        // gameManager.currentContainer = null;
    }
}
