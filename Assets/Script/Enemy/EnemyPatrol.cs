using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float range;
    [SerializeField] float moveSpeed;

    //* Component
    Rigidbody2D rigidbody2D;
    Animator animator;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Catch player is destroy
        try
        {
            // Distance to player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= range)
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
            print("Player is Destroy");
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
        rigidbody2D.velocity = new Vector2(0, 0);
        animator.SetBool("walk", false);
    }
}
