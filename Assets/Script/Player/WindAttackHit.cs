using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAttackHit : MonoBehaviour
{
    //* Component
    ProjectileController projectileController;
    public GameObject WindExplosion;
    //* Variable
    private float attackDamage = 1f;

    private void Awake()
    {
        //* Get projectile controller parent component
        projectileController = GetComponentInParent<ProjectileController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //* Check attack is collide with Enemy or Ground
        if (other.gameObject.tag == "Trap" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
        {
            //* Stop the wind attack
            projectileController.RemoveAttack();
            //* Create a explosion game object
            Instantiate(WindExplosion, transform.position, transform.rotation);
            //* Destroy wind attack
            Destroy(gameObject);
            //* Take dame to Enemy
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
                enemyController.enemyTakeDamage(attackDamage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Trap" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
        {
            projectileController.RemoveAttack();
            Instantiate(WindExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
