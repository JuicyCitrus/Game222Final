using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

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

    [SerializeField] GameObject selectedButton;

    [SerializeField]
    private List<GameObject> playerParts;

    [SerializeField]
    private AudioSource deathSound;

    [SerializeField]
    private Scoring scoreboard;

    public static Action PlayerDeath = delegate { };

    public bool isDead = false;
    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();
    private Quaternion SpawnRot;

    private void Awake()
    {
        foreach (GameObject part in playerParts)
        {
            positions.Add(part.transform.localPosition);
            rotations.Add(part.transform.localRotation);
        }
        SpawnRot = this.transform.rotation;
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
            // Explosion
            foreach (GameObject part in playerParts)
            {
                part.AddComponent<Rigidbody>();
                part.GetComponent<Rigidbody>().useGravity = true;
                part.GetComponent<Rigidbody>().isKinematic = false;
            }

            // Delegate Call
            PlayerDeath();

            // Death Audio
            deathSound.Play();

            // Stop the Scoreboard 
            scoreboard.StopScoring();
            
            // Disable Player Input
            gameObject.GetComponent<PlayerShootTap>().enabled = false;
            gameObject.GetComponent<CharacterController>().enabled = false;

            // Death Menu Set Up
            DeathScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(selectedButton);

            // Bool Control
            isDead = true;
            this.GetComponent<MovementScript>().isDead = true;
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
        // Disable the character controller to allow for the spawn point to be reset
        if (gameObject.GetComponent<CharacterController>().enabled)
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
        }

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
        transform.position = new Vector3(0, 0, 0);
        this.transform.rotation = SpawnRot;
        for (int i = 0; i < playerParts.Count; i++)
        {
            playerParts[i].transform.localPosition = positions[i];
            playerParts[i].transform.localRotation = rotations[i];
        }

        // Enable the player control scripts
        this.GetComponent<MovementScript>().isDead = false;
        gameObject.GetComponent<PlayerShootTap>().enabled = true;
        gameObject.GetComponent<CharacterController>().enabled = true;
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
