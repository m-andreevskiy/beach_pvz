using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Castle_wall : CastleBase
{

    public float health;
    [SerializeField] private int maxHealth = 300;
    public Vector2 position;

    [SerializeField] private GameObject tooltipObject;
    private CastleTooltip tooltip;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        // tooltipObject.SetActive(false);

    }

    override public void Init()
    {
        int finalHealth = Math.Max(60, maxHealth - (int)(TOTAL_BUILDING_INACCURACY * 30));
        health = finalHealth;
        healthScript.Init(finalHealth, finalHealth);
    }


    // Update is called once per frame
    void Update()
    {
        
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
        // tooltipObject.SetActive(true);
    }

    void OnMouseExit()
    {
        // tooltipObject.SetActive(false);
    }
}
