using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    //* Time to alive
    float timer = 0.4f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //* Get player position and local scale
        transform.position = PlayerController.Instance.transform.position;
        transform.localScale = PlayerController.Instance.transform.localScale;
        //* Set sprite of ghost is player sprite
        spriteRenderer.sprite = PlayerController.Instance.playerSprite.sprite;
        //* Change color look like ghost by rgba
        spriteRenderer.color = new Vector4(50f, 50f, 50f, 1f);
    }

    private void Update()
    {
        //* Set time to destroy the ghost sprite
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
