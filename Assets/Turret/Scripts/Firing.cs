using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRate : MonoBehaviour
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

    private List<Bullet> bullets = new List<Bullet>();

    private bool bActive;

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
        StartCoroutine(nameof(Firing));
        StartCoroutine(nameof(Aiming));
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

    IEnumerator Firing()
    {
        // Bullet bullet;

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



            /* bullet = Instantiate(bulletPreFab, spawnPoint).GetComponent<Bullet>();
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.parent = null;
            bullet.Activate(speed, transform.forward); */
        }
    }
}

