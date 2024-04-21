/* using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private int bulletCache = 5;
    
    [SerializeField]
    private float speed = 20;

    [SerializeField]
    private float fireRate = 1;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private GameObject bulletPreFab;

    private List<Bullet> bullets = new List<Bullet>();

    private bool bActive;
    private PlayerControls pInput;

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
    }

    private void Update()
    {

        // If player is pressing the button for fire, start shooting
        if (pInput.Player.Fire.)
        {
            if(bActive == true)
            {
                bActive = false;
            }
        }
        // If player enters range, start firings
        else if (distanceToPlayer < 32)
        {
            if(bActive != true)
            {
                bActive = true;
                StartCoroutine(nameof(Shooting));
            }
        }
    }

    IEnumerator Shooting()
    {
        while (bActive)
        {
            yield return new WaitForSeconds(fireRate);

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
} */