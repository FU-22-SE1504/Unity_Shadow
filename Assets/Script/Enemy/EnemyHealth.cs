using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    //* Component
    public Slider healthBarSlider;
    //* Variable
    public Color Low;
    public Color Hight;
    public Vector3 Offset;

    public void SetHealth(float currentHealth, float maxHealth)
    {
        //* Setting the sliders
        healthBarSlider.gameObject.SetActive(currentHealth < maxHealth);
        //* Set default and max health
        healthBarSlider.value = currentHealth;
        healthBarSlider.maxValue = maxHealth;
        //* Show the health bar
        healthBarSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, Hight, healthBarSlider.normalizedValue);
    }


    private void Update()
    {
        //* Transform position and plus to new Vector
        healthBarSlider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
