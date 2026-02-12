using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class CastleBase : MonoBehaviour
{
    public GameManager gameManager;
    public int line;
    public float TOTAL_BUILDING_INACCURACY;
    public bool isBuilt = false;
    public virtual bool isChild { get;} = false;
    public ObjectContainer assignedContainer;
    [SerializeField] private GameObject tooltipObject;
    [SerializeField] protected int costInSand;
    [SerializeField] protected int costInClay;
    [SerializeField] protected int costInPearls;

    protected Health healthScript;

    protected virtual void Start()
    {
        healthScript = GetComponent<Health>();
        tooltipObject.SetActive(false);
    }

    public virtual void Init()
    {
        Debug.Log("You shouldn't see this. Implement 'Init' function in your castle");
    }

    public int getCostInSand()
    {
        return costInSand;
    }

    public int getCostInClay()
    {
        return costInClay;
    }

    public int getCostInPearls()
    {
        return costInPearls;
    }

    public virtual string GetTooltipInfo()
    {
        string res;
        res = "Health " + healthScript.getHealth() + " / " + healthScript.getMaxHealth();

        return res;
    }


    private void OnDestroy()
    {
        assignedContainer.isFull = false;
    }


    virtual protected void OnMouseEnter()
    {
        tooltipObject.SetActive(true);
    }

    virtual protected void OnMouseExit()
    {
        tooltipObject.SetActive(false);
    }

}
