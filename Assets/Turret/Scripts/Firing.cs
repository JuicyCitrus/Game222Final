using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class Firing: MonoBehaviour
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
    public float zCoord;

    // This is called once per session.
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

        bActive = true;
        StartCoroutine(nameof(Shooting));
        StartCoroutine(nameof(Aiming));
    }

    private void Update()
    {
        // Get turret coordinates
        xCoord = this.transform.position.x;
        zCoord = this.transform.position.z;

        // Generate values for the distance formula from turret to player
        float xDist = (player.xCoord - xCoord) * (player.xCoord - xCoord);
        float zDist = (player.zCoord - zCoord) * (player.zCoord - zCoord);

        // Perform distance formula calculation
        distanceToPlayer = Mathf.Sqrt(xDist + zDist);

        // If player is out of range, stop firing
        if(distanceToPlayer >= 32)
        {
            bActive = false;
        }
    }

    IEnumerator Aiming()
    {
        Vector3 direction;

        while (bActive)
        {
            direction = target.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);

            yield return new WaitForEndOfFrame();
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
}

