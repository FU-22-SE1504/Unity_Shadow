using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [Header("Explode Effect")]
    public GameObject explodeEffect;

    [Header("Attack")]
    Rigidbody2D rigidbody2D;
    public float attackSpeed;
    public float attackDamage;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = transform.right * attackSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector3 rotation = new Vector3(0f, 0f, -90f);
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.PlayerTakeDamage(attackDamage);
            Instantiate(explodeEffect, transform.position, Quaternion.Euler(0, 0, -90));
            Destroy(gameObject);
        }
    }
}
