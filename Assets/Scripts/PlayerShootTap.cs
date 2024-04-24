using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShootTap : MonoBehaviour
{
    [SerializeField]
    private int bulletCache = 5;

    [SerializeField]
    private float speed = 20;

    [SerializeField]
    private Transform rightSpawnPoint;

    [SerializeField]
    private Transform leftSpawnPoint;

    [SerializeField]
    private GameObject bulletPreFab;

    private List<BulletPlayer> rightBullets = new List<BulletPlayer>();
    private List<BulletPlayer> leftBullets = new List<BulletPlayer>();
    private PlayerControls pInput;

    private void Awake()
    {
        pInput = new PlayerControls();
        pInput.Enable();
        pInput.Player.Fire.performed += EnableFire;
    }
    private void OnDisable()
    {
        pInput.Player.Fire.performed -= EnableFire;
    }

    private void Start()
    {
        // Instantiate all of the bullets
        for (int i = 0; i < bulletCache; i++)
        {
            // A bullet gets instantiated and added to the list of bullets
            rightBullets.Add(Instantiate(bulletPreFab).GetComponent<BulletPlayer>());
            leftBullets.Add(Instantiate(bulletPreFab).GetComponent<BulletPlayer>());
        }

        // Disable all bullets in list to start
        foreach (BulletPlayer bullet in rightBullets)
        {
            bullet.gameObject.SetActive(false);
        }
        foreach (BulletPlayer bullet in leftBullets)
        {
            bullet.gameObject.SetActive(false);
        }
    }
    private void EnableFire(InputAction.CallbackContext c)
    {
        // Find First Inactive Bullet
        foreach (BulletPlayer b in rightBullets)
        {
            if (!b.BActive)
            {
                // Turn on Bullets
                b.gameObject.SetActive(true);

                // Set Position of Bullet
                b.transform.position = rightSpawnPoint.position;

                // Activate Bullet
                b.Activate(speed, transform.forward);
                break;
            }
        }
        foreach (BulletPlayer b in leftBullets)
        {
            if (!b.BActive)
            {
                // Turn on Bullets
                b.gameObject.SetActive(true);

                // Set Position of Bullet
                b.transform.position = leftSpawnPoint.position;

                // Activate Bullet
                b.Activate(speed, transform.forward);
                break;
            }
        }
    }
}