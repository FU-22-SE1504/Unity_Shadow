using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    //* Variable
    private float damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //* Get component
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            //* Make damage to player
            playerHealth.PlayerTakeDamage(damage);
        }
    }
}
