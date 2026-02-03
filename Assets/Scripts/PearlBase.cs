using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PearlBase : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] int value = 5;
    private Animator animator;
    private bool isClicked = false;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    void OnMouseDown()
    {
        if (isClicked)
        {
            return;
        }

        isClicked = true;
        gameManager.AddResource("pearls", value);
        animator.SetTrigger("collected");
    }

    public void OnCollectAnimaitonEnd()
    {
        Destroy(gameObject);
    }

}
