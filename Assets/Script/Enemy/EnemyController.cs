using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //* Component
    public EnemyHealth enemyHealth;
    private Animator animator;
    private Rigidbody2D rigidbody2D;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask playerLayer;
    private PlayerHealth playerHealth;
    //* Variable
    public float currentHealth;
    public float maxHealth = 5f;
    public float delayDeath = 0f;
    //* AI
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private int attackDamage = 2;
    [SerializeField] private float colliderDistance = 1f;
    [SerializeField] private float range = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        enemyHealth.SetHealth(currentHealth, maxHealth);
        rigidbody2D = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        //* Attack only when player in sight 
        if (playerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                //* Attack
                cooldownTimer = 0;
                animator.SetTrigger("attack");
                rigidbody2D.velocity = new Vector2(0, 0);
            }
        }
    }

    public void enemyTakeDamage(float damage)
    {
        //* Take dame
        currentHealth -= damage;
        animator.SetTrigger("hurt");
        //* Set slider health
        enemyHealth.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            animator.SetBool("isDeath", true);
            // Will destroy when finish animation
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + delayDeath);
        }
    }
    //* Check distance of enemy with player
    private bool playerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center + transform.right * range * transform.localScale.x * -colliderDistance,
        new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z),
        0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<PlayerHealth>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * range * transform.localScale.x * -colliderDistance,
        new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
    }

    private void DamagePlayer()
    {
        //* If player still in sight => damage player
        if (playerInSight())
        {
            playerHealth.PlayerTakeDamage(attackDamage);
        }
    }
}

