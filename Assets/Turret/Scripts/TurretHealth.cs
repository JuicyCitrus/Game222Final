using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using UnityEngine;

public class TurretHealth : MonoBehaviour
{
    [SerializeField]
    public float hp;

    [SerializeField]
    public int maxHealth = 10;

    [SerializeField]
    private HealthHUD HH;

    [SerializeField]
    private GameObject turret;

    public bool isDead;
    public static Action TurretDeath = delegate { };

    private void Start()
    {
        // Set HP to full at the start of the game
        turret.SetActive(true);
        this.GetComponent<BoxCollider>().enabled = true;
        isDead = false;
        hp = maxHealth;
        HH.UpdateHealthBar(hp / maxHealth);
    }

    private void Update()
    {
        // Death actions
        if (hp <= 0 && isDead == false)
        {
            TurretDeath();
            this.GetComponent<BoxCollider>().enabled = false;
            turret.SetActive(false);
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

    private void OnEnable()
    {
        ResetButton.ResetScene += Start;
    }

    private void OnDisable()
    {
        ResetButton.ResetScene -= Start;
    }
}
