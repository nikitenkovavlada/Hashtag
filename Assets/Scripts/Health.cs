using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;
    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) { Die(); }

        healthBar.SetHealth(currentHealth);
    }
    public void Heal(int heal)
    {
        if (currentHealth + heal > maxHealth) { currentHealth = maxHealth; }
        else { currentHealth += heal; }

        Debug.Log(heal);
        healthBar.SetHealth(currentHealth);
    }
    private void Die()
    {   Debug.Log("Died");
        Destroy(gameObject);
    }
}
