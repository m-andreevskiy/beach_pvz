using System;
using UnityEngine;


[RequireComponent(typeof(Health))]
public class Castle : CastleBase
{

    public LayerMask enemyMask;
    public GameObject projectilePrefab;
    public float health;
    [SerializeField] private int maxHealth = 100;
    public int damage;
    public int maxDamage = 100;
    public float attackSpeed = 10;
    public Vector2 position;

    [SerializeField] private GameObject tooltipObject;
    private CastleTooltip tooltip;
    private float attackTimer = 0;
    private float baseAttackTime = 10;


    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tooltipObject.SetActive(false);

    }

    override public void Init()
    {
        int finalHealth = Math.Max(5, maxHealth - (int)(TOTAL_BUILDING_INACCURACY * 10));
        health = finalHealth;
        damage = Math.Max(5, maxDamage - (int)(TOTAL_BUILDING_INACCURACY * 10));
        healthScript.Init(finalHealth, finalHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBuilt)
        {
            return;
        }

        attackTimer += Time.deltaTime;

        if (gameManager != null)
        {
            if (attackSpeed != 0 && isBuilt && attackTimer >= baseAttackTime / attackSpeed)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1000, enemyMask);
                if (hit)
                {
                    // print ("seeing " + hit.collider.gameObject.name);
                    attackTimer = 0;
                    Shoot();
                }
                else {
                    // print ("hit is null");
                }
            }
        }
    }

    override public string GetTooltipInfo()
    {
        string res;
        res = "Health " + healthScript.getHealth() + " / " + healthScript.getMaxHealth();
        res += "\nDamage " + damage + " / " + maxDamage;

        return res;

    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, this.transform);
        projectile.GetComponent<ProjectileBase>().SetDamage(damage);
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
