using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpHeight = 12;
    [SerializeField] float moveSpeed = 5;
    private enum MovementState { idle, moving, jumping, falling }
    private MovementState state = MovementState.idle;
    private bool grounded;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                grounded = false;
                GetComponent<Rigidbody2D>().velocity = new Vector3(rb.velocity.x, jumpHeight, transform.localPosition.z);
            }
        }
        Animation(x);
    }

    private void Animation(float x)
    {
        MovementState state;
        if (x > 0f)
        {
            state = MovementState.moving;
            // Change flip
            sprite.flipX = false;
        }
        else if (x < 0f)
        {
            state = MovementState.moving;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
