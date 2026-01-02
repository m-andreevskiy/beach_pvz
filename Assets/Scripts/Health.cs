using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

  [SerializeField] private int maxHealth;
  [SerializeField] private int health;
  // [SerializeField] private GameObject healthBar;


  public void init()
  {

  }

  private void updateHealthBar()
  {
    // healthBar.transform.localScale = new Vector3((float)health / (float)maxHealth, 1, 1);
  }

  void Start()
  {
    health = maxHealth;
    // updateHealthBar();
  }


  public void Damage(int damage)
  {
    health -= damage;
    if (health <= 0)
    {
      if (gameObject.CompareTag("Player"))
      {
        // StartCoroutine(GetComponent<PlayerControls>().restart());
      }
      else
      {
        Die();
      }
    }

    // updateHealthBar();
  }

  private void Die()
  {
    Debug.Log("Someone's dead");
    Destroy(gameObject);
  }

  public int getHealth()
  {
    return health;
  }

  public void setHealth(int desiredHealth)
  {
    int newHealth = desiredHealth;
    if (newHealth < 0)
    {
      newHealth = 0;
    }
    else if (newHealth > maxHealth)
    {
      newHealth = maxHealth;
    }
    if (newHealth > 0 && newHealth <= maxHealth)
    {
      health = newHealth;
      // updateHealthBar();
    }
  }
}
