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
    [SerializeField]
    private MovementScript MS;
    [SerializeField]
    private GameObject DeathScreen;

    private void Start()
    {
        // Set HP to full at the start of the game
        hp = maxHealth;
    }

    private void Update()
    {
        // Death actions
        if(hp <= 0)
        {
            // MS.enabled = false;
            DeathScreen.SetActive(true);
            Time.timeScale = 0;
        }
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
