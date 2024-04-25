using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

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
    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();
    private Quaternion SpawnRot;
    private Vector3 CCspawn;

    private void Awake()
    {
        foreach (GameObject part in playerParts)
        {
            positions.Add(part.transform.localPosition);
            rotations.Add(part.transform.localRotation);
        }
        SpawnRot = this.transform.rotation;
        CCspawn = this.GetComponent<CharacterController>().transform.position;
    }

    private void Start()
    {
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

    private void Respawn()
    {
        // Remove rigidbodies
        if (isDead == true)
        {
            foreach (GameObject part in playerParts)
            {
                Rigidbody rb = part.GetComponent<Rigidbody>();
                Destroy(rb);
            }
        }
        isDead = false;

        // Set up HP and HP bar
        hp = maxHealth;
        HH.UpdateHealthBar(hp);

        // Get rid of the death screen
        DeathScreen.SetActive(false);

        // Reset the positions of the parts
        transform.localPosition = new Vector3(0, 0, 0);
        this.transform.rotation = SpawnRot;
        for (int i = 0; i < playerParts.Count; i++)
        {
            playerParts[i].transform.position = positions[i];
            playerParts[i].transform.rotation = rotations[i];
        }
        this.GetComponent<CharacterController>().transform.position = CCspawn;

        // Enable the player control scripts
        gameObject.GetComponent<MovementScript>().enabled = true;
        gameObject.GetComponent<PlayerShootTap>().enabled = true;
    }

    private void OnEnable()
    {
        ResetButton.ResetScene += Respawn;
    }

    private void OnDisable()
    {
        ResetButton.ResetScene -= Respawn;
    }
}
