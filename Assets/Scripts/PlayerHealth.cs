using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    public float hp;
    [SerializeField]
    public int maxHealth = 10;
    [SerializeField]
    private HealthHUD HH;

    private void Start()
    {
        // Set HP to full at the start of the game
        hp = maxHealth;
    }

    public void UpdateHealth(float value)
    {
        // Subtract the damage from our in game HP
        hp -= value;

        // Update the UI's health bar to be sized at the same ratio as our HP compared to our maximum HP stat
        HH.UpdateHealthBar(hp / maxHealth);
    }
    public void ResetHealth()
    {
        // Reset the our HP to our max HP stat when the function is called.
        hp = maxHealth;
    }
}
