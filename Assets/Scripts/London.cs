using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireRate : MonoBehaviour
{

    [SerializeField]
    private int bulletCacheCount = 100;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float FireDelay = 0.1f;

    [SerializeField]
    private float fireRate = 0.1f;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject bulletPrefab;
    //[SerializeField]
    //private Transform target;
    private List<Bullet> bullets = new List<Bullet>();

    private bool bActive;

    private PlayerControls inputActions;
    private CharacterController playerController;
    private bool activateShoot;

    //this is called once per session
    private void Start()
    {
        //instantiate ALL of the bullets
        for (int i = 0; i < bulletCacheCount; i++)
        {
            //spawn      item          get something from item    
            bullets.Add(Instantiate(bulletPrefab).GetComponent<Bullet>());
            bullets[i].gameObject.SetActive(false);
        }

        inputActions = new PlayerControls();
        inputActions.Enable();
        playerController = GetComponent<CharacterController>();

        inputActions.Player.Fire.performed += EnableShoot;
        inputActions.Player.Fire.canceled += DisableShoot;
    }

    private void Update()
    {

        if (activateShoot)
        {
            bActive = true;
            StartCoroutine(nameof(Firing));

        }
        else
        {
            bActive = false;
        }
    }

    public IEnumerator Firing()
    {
        //Bullet bullet;

        while (bActive)
        {
            yield return new WaitForSeconds(FireDelay);

            //find the first inactive            
            foreach (Bullet b in bullets)
            {
                if (!b.BActive)
                {
                    //turn on bullets
                    b.gameObject.SetActive(true);
                    yield return new WaitForSeconds(fireRate);
                    //set position of bullet
                    b.transform.position = spawnPoint.position;
                    //activate bullet
                    b.Activate(speed, transform.forward);
                    break;
                }
            }

            #region Alternate 
            /*bullet =  Instantiate(bulletPrefab,spawnPoint).GetComponent<Bullet>();
            bullet.transform.localPosition = Vector3.zero; //moves to parent exact location
            bullet.transform.parent = null;
            bullet.Activate(speed, transform.forward);*/
            #endregion
        }
    }

    private void EnableShoot(InputAction.CallbackContext c)
    {
        activateShoot = true;
    }
    private void DisableShoot(InputAction.CallbackContext c)
    {
        activateShoot = false;
    }

    private void OnDisable()
    {
        inputActions.Player.Fire.performed -= EnableShoot;
        inputActions.Player.Fire.canceled -= DisableShoot;

    }
}
