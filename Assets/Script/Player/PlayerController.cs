using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //** Component
    #region 
    Rigidbody2D rigidbody2D;
    Animator animator;
    public SpriteRenderer playerSprite;
    #endregion
    //** General Variable
    [SerializeField] float movingSpeed = 10f;
    [SerializeField] float jumpHeight = 15f;
    private bool turnAround;
    private bool grounded;
    //* Player Ghost variable
    #region 
    private static PlayerController instance;
    [SerializeField] GameObject playerGhost;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerController>();
            }
            return instance;
        }
    }
    #endregion
    //* Dash variable
    #region 
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    #endregion
    //* Attack variable
    #region 
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject windSprite;
    private float attackCooldown = 1f;
    private float nextAttack = 0;
    #endregion
    private void Start()
    {
        //* Declare component
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        //* Set player not turn around
        turnAround = false;
    }

    private void Update()
    {
        //* Check player in dashing
        if (isDashing)
        {
            return;
        }

        //* Get keyboard input
        float move = Input.GetAxis("Horizontal");

        //* Moving
        rigidbody2D.velocity = new Vector2(move * movingSpeed, rigidbody2D.velocity.y);

        //* Check turn around
        if (move > 0 && turnAround)
        {
            Flip();
        }
        else if (move < 0 && !turnAround)
        {
            Flip();
        }

        //* Jump
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            grounded = false;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);
            animator.SetTrigger("jump");
        }

        //* Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            //* Ghost Effect
            GameObject GhostEffect = Instantiate(playerGhost, transform.position, transform.rotation);
            //* Start Dash
            StartCoroutine(Dash());
        }

        //* Attack
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        //* Animation
        animator.SetBool("move", move != 0);
        animator.SetBool("grounded", grounded);
    }

    //* Change the direction of the character movement
    private void Flip()
    {
        turnAround = !turnAround;
        //* Get direction recent 
        Vector3 localScale = transform.localScale;
        //* Change direction
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    //* Check tag
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    //* Dash logic
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rigidbody2D.gravityScale;
        //* Reset gravity to avoid fall down when dashing
        rigidbody2D.gravityScale = 0;
        //* Change the player position
        rigidbody2D.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        //* Return original gravity
        rigidbody2D.gravityScale = originalGravity;
        isDashing = false;
        //* Dashing cooldown
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    //* Attack logic
    private void Attack()
    {
        //* Check player can attack
        if (Time.time > nextAttack)
        {
            //* Animation
            animator.SetTrigger("attack");
            //* Set attack cooldown
            nextAttack = Time.time + attackCooldown;
            if (!turnAround)
            {
                //* Create a wind attack
                //? what Quaternion can do 
                Instantiate(windSprite, attackPoint.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else if (turnAround)
            {
                Instantiate(windSprite, attackPoint.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            }
        }
    }
}

