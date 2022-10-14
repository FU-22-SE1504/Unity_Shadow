using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    //* Component
    Rigidbody2D rigidbody2D;
    //* Variable
    [SerializeField] private float windSpeed = 50f;

    private void Awake()
    {
        //* Get component
        rigidbody2D = GetComponent<Rigidbody2D>();
        //* Create a sprite position
        if (transform.rotation.z > 0)
        {
            rigidbody2D.AddForce(new Vector2(-1, 0) * windSpeed, ForceMode2D.Impulse);
        }
        else
        {
            rigidbody2D.AddForce(new Vector2(1, 0) * windSpeed, ForceMode2D.Impulse);
        }
    }

    //* Stop the wind attack
    public void RemoveAttack()
    {
        rigidbody2D.velocity = new Vector2(0, 0);
    }

}
