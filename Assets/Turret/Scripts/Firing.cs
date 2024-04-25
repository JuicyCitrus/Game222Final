using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Firing : MonoBehaviour
{
    [SerializeField]
    private int bulletCache = 100;

    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private float fireRate = 0.1f;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private GameObject bulletPreFab;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private MovementScript player;

    private List<Bullet> bullets = new List<Bullet>();

    private float distanceToPlayer;
    private bool bActive;

    public float xCoord;
    public float yCoord;
    public float zCoord;

    private void OnEnable()
    {
        PlayerHealth.PlayerDeath += Ceasefire;
        ResetButton.ResetScene += restartShooting;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDeath -= Ceasefire;
        ResetButton.ResetScene -= restartShooting;
    }

    private void Start()
    {
        // Instantiate all of the bullets
        for (int i = 0; i < bulletCache; i++)
        {
            // A bullet gets instantiated and added to the list of bullets
            bullets.Add(Instantiate(bulletPreFab).GetComponent<Bullet>());
        }

        // Disable all bullets in list
        foreach (Bullet bullet in bullets)
        {
            bullet.gameObject.SetActive(false);
        }

        bActive = false;
        StartCoroutine(nameof(Aiming));
        StartCoroutine(nameof(Shooting));
    }

    private void Update()
    {
        // Get turret coordinates
        xCoord = this.transform.position.x;
        yCoord = this.transform.position.y;
        zCoord = this.transform.position.z;

        // Generate values for the distance formula from turret to player
        float xDist = (player.xCoord - xCoord) * (player.xCoord - xCoord);
        float zDist = (player.zCoord - zCoord) * (player.zCoord - zCoord);

        // Perform distance formula calculation
        distanceToPlayer = Mathf.Sqrt(xDist + zDist);

        // If player is out of range, stop firing
        if (distanceToPlayer >= 32 || Mathf.Abs(player.yCoord - yCoord) > 2)
        {
            if (bActive == true)
            {
                bActive = false;
            }
        }
        // If player enters range, start firings
        else if (distanceToPlayer < 32 && Mathf.Abs(player.yCoord - yCoord) < 2)
        {
            if (bActive != true)
            {
                bActive = true;
            }
        }
    }

    private void restartShooting()
    {        
        Debug.Log("restartShooting called");

        bActive = false;
        StartCoroutine(nameof(Aiming));
        StartCoroutine(nameof(Shooting));
    }

    private void Ceasefire()
    {
        StopAllCoroutines();
    }

    IEnumerator Aiming()
    {
        Vector3 direction;

        while (true)
        {
            if (bActive)
            {
                direction = target.position - transform.position;
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction);
            }

            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator Shooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);

            if (bActive) 
            {
                // Find First Inactive Bullet
                foreach (Bullet b in bullets)
                {
                    if (!b.BActive)
                    {
                        // Turn on Bullets
                        b.gameObject.SetActive(true);

                        // Set Position of Bullet
                        b.transform.position = spawnPoint.position;

                        // Activate Bullet
                        b.Activate(speed, transform.forward);
                        break;
                    }
                }
            }
        }
    }
}

