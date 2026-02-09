using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class ChildBase : CastleBase
{

    public override bool isChild { get;} = true;
    public float health;
    public string resourceName;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject childTooltip;
    [SerializeField] private GameObject resourceModal;

    [SerializeField] private GameObject sandFadeAwayPrefab;
    [SerializeField] private GameObject clayFadeAwayPrefab;
    [SerializeField] private float generationRate = 2;

    private float BASE_GENERATION_TIME = 10;
    private float generationTimer = 0;
 

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
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

            switch (resourceName)
            {
                case "sand":
                    Instantiate(sandFadeAwayPrefab, transform);
                    break;

                case "clay":
                    Instantiate(clayFadeAwayPrefab, transform);
                    break;

                default:
                    break;
            }
        }

    }

    override public string GetTooltipInfo()
    {
        string res;
        res = "Health " + healthScript.getHealth() + " / " + healthScript.getMaxHealth();
        res += "\nDamage 0";

        return res;

    }


    override protected void OnMouseEnter()
    {
        if (resourceModal.activeInHierarchy)
        {
            return;
        }
        
        base.OnMouseEnter();
        childTooltip.SetActive(true);
    }

    override protected void OnMouseExit()
    {
        base.OnMouseExit();
        childTooltip.SetActive(false);
    }

    void OnMouseUp()
    {
        OnMouseExit();
        resourceModal.SetActive(true);
    }

}
