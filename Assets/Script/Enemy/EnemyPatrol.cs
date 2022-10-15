using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float range;
    [SerializeField] float moveSpeed;

    private bool canMove = true;

    //* Component
    Rigidbody2D rigidbody2D;
    Animator animator;
    EnemyController enemyController;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }

    private void Update()
    {
        // Catch player is destroy
        try
        {
            // Distance to player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            //Check distance and enemy can not move while attack and death
            if (distanceToPlayer <= range && canMove)
            {
                // Follow to attack player
                ChasePLayer();
            }
            else
            {
                // Stop chase
                StopChasePlayer();
            }
        }
        catch (Exception e)
        {
            print("Player is Destroy" + e);
            StopChasePlayer();
        }
    }

    public void ChasePLayer()
    {
        // Check position between player and enemy
        if (transform.position.x < player.position.x)
        {
            // Move to player
            rigidbody2D.velocity = new Vector2(moveSpeed, 0);
            //Flip
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(-moveSpeed, 0);
            //Flip
            transform.localScale = new Vector2(1, 1);
        }
        animator.SetBool("walk", true);

    }

    public void StopChasePlayer()
    {
        // Stop enemy when distance between player and enemy so hight
        rigidbody2D.velocity = new Vector2(0, 0);
        animator.SetBool("walk", false);
    }

    public void enemyCanMove()
    {
        canMove = true;
    }

    public void enemyStopMove()
    {
        canMove = false;
    }
}
