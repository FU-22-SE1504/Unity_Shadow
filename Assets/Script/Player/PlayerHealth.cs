using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //* Component

    public GameObject DeathEffect;
    //* Variable
    private float maxHealth = 10;
    float currentHealth;

    private void Start()
    {
        //* Set default health
        currentHealth = maxHealth;
        //* Set default health bar

    }

    //* Player get damage
    public void PlayerTakeDamage(float damage)
    {
        //* Check player death
        if (currentHealth <= 0)
        {
            PlayerDeath();
        }
        //* Health loss

        //* Take dame
        currentHealth -= damage;
    }

    //* Player Death
    public void PlayerDeath()
    {
        //* Create a death effect
        Instantiate(DeathEffect, transform.position, transform.rotation);
        //* Destroy player
        Destroy(gameObject);
    }

}
