using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class ChildBase : CastleBase
{

    public override bool isChild { get; set;} = true;
    public float health;
    public string resourceName;
    [SerializeField] private int maxHealth = 100;
    // [SerializeField] private GameManager gameManager;
    public Vector2 position;

    [SerializeField] private GameObject tooltipObject;
    private CastleTooltip tooltip;

    [SerializeField] private float generationRate = 2;

    private float BASE_GENERATION_TIME = 10;
    private float generationTimer = 0;


    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        tooltipObject.SetActive(false);
        healthScript.Init(maxHealth, maxHealth);

    }



    // Update is called once per frame
    void Update()
    {
        generationTimer += Time.deltaTime;

        if (generationTimer >= BASE_GENERATION_TIME / generationRate)
        {
            generationTimer = 0;

            gameManager.AddResource(resourceName, 1);
        }

    }

    override public string GetTooltipInfo()
    {
        string res;
        res = "Health " + healthScript.getHealth() + " / " + healthScript.getMaxHealth();
        res += "\nDamage 0";

        return res;

    }

    void OnMouseEnter()
    {
        tooltipObject.SetActive(true);
    }

    void OnMouseExit()
    {
        tooltipObject.SetActive(false);
    }
}
