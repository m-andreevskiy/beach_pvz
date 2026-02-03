using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] LayerMask castleLayerMask;
    [SerializeField] private int damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float speed;



    private Animator animator;
    private RaycastHit2D castleRaycastHit;
    private float SPEED_MULTIPLIER = 0.05F;
    private float attackTimer = 0;
    private float baseAttackTime = 10;
    private Health castleHealth;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnDeathAnimationStart()
    {
        // collider.enabled = false;
    }
    public void OnDeathAnimaitonEnd()
    {
        Destroy(gameObject);

    }

    private void FixedUpdate()
    {
        castleRaycastHit = Physics2D.Raycast(transform.position, Vector2.left, 0.2F, castleLayerMask);
        if (castleRaycastHit) 
        {
            animator.SetBool("isAttacking", true);
            attackTimer += Time.deltaTime;
            if (attackTimer >= baseAttackTime / attackSpeed)
            {
                castleHealth = castleRaycastHit.collider.gameObject.GetComponent<Health>();
                if (castleHealth)
                {
                    castleHealth.Damage(damage);
                }
                attackTimer = 0;
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);
            attackTimer = 0;
            transform.position = new Vector3(transform.position.x - speed * SPEED_MULTIPLIER * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
}
