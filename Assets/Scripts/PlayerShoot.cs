using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
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

    private void Awake()
    {
        pInput = new PlayerControls();
        pInput.Enable();
        pInput.Player.Fire.performed += EnableFire;
        pInput.Player.Fire.canceled += DisableFire;
    }
    private void OnDisable()
    {
        pInput.Player.Fire.performed -= EnableFire;
        pInput.Player.Fire.canceled -= DisableFire;
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
        StartCoroutine(nameof(Shooting));
    }
    private void EnableFire(InputAction.CallbackContext c)
    {
        bActive = true;
    }
    private void DisableFire(InputAction.CallbackContext c)
    {
        bActive = false;
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            if (bActive)//toggles firing
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