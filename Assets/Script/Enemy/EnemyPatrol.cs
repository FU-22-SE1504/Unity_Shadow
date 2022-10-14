using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float walkSpeed;
    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;
    public LayerMask groundLayer;
    public Transform groundCheckPos;
    private Rigidbody2D rigidbody2D;
    private Animator animator;

    private void Start()
    {
        mustPatrol = true;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }

    private void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
        animator.SetBool("walk", mustPatrol);
    }

    private void Patrol()
    {
        if (mustTurn)
        {
            Flip();
        }
        rigidbody2D.velocity = new Vector2(-walkSpeed * Time.fixedDeltaTime, -rigidbody2D.velocity.y);
    }

    private void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.lossyScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

}
