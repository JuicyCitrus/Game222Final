using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private List<GameObject> playerParts;

    [SerializeField]
    private AudioSource deathSound;

    [SerializeField]
    private Scoring scoreboard;

    public static Action PlayerDeath = delegate { };

    private bool isDead = false;

    private void Start()
    {
        // Set HP to full at the start of the game
        hp = maxHealth;
    }

    private void Update()
    {
        // Death Actions
        if(hp <= 0 && !isDead)
        {
            foreach (GameObject part in playerParts)
            {
                part.AddComponent<Rigidbody>();
                part.GetComponent<Rigidbody>().useGravity = true;
                part.GetComponent<Rigidbody>().isKinematic = false;
            }
            PlayerDeath();
            deathSound.Play();
            scoreboard.StopScoring();   
            gameObject.GetComponent<MovementScript>().enabled = false;
            gameObject.GetComponent<PlayerShootTap>().enabled = false;
            DeathScreen.SetActive(true);
            isDead = true;
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
