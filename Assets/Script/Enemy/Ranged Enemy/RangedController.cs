using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangedController : MonoBehaviour
{
    [Header("Enemy")]
    private Animator animator;
    private Rigidbody2D rigidbody2D;
    public float rangeToPlayer;
    private float delayDeath = 0f;

    [Header("Health")]
    public EnemyHealth enemyHealth;
    public float currentHealth;
    public float maxHealth = 5f;
    [Header("Player")]
    [SerializeField] Transform player;
    private PlayerHealth playerHealth;
    [Header("Attack")]
    private float nextAttack = 0f;
    public float attackCooldown = 2f;
    public Transform firePoint;
    public GameObject fireSprite;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        enemyHealth.SetHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        try
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= rangeToPlayer)
            {
                if (Time.time > nextAttack)
                {
                    animator.SetTrigger("attack");
                    
                    nextAttack = Time.time + attackCooldown;
                }
            }
        }
        catch (Exception e)
        {
            print(e);
        }
    }

    public void rangedTakeDame(float damage)
    {
        //* Take dame
        currentHealth -= damage;
        animator.SetTrigger("hurt");
        //* Set slider health
        enemyHealth.SetHealth(currentHealth, maxHealth);
        //* Enemy death
        if (currentHealth <= 0)
        {
            Destroy(enemyHealth.gameObject);
            animator.SetBool("isDeath", true);
        }
    }

    public void DestroyAnimationDeath()
    {
        Destroy(gameObject);
    }

    public void RangedCreateAttack()
    {
        Instantiate(fireSprite, firePoint.position, firePoint.rotation);
    }
}
