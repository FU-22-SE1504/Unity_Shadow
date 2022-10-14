using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //* Component
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(float maxHealth)
    {
        //* Set default health bar
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        //* Set gradient color
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        //* Set current color
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
