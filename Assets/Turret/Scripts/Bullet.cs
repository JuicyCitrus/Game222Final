using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(AudioSource))]

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private AudioSource collisionSound;
    [SerializeField] 
    private AudioSource firingSound;

    [SerializeField]
    private Renderer mRenderer;
    [SerializeField]
    private Collider cCollider;
    [SerializeField]
    private float lifetime = 5;

    private float speed = 5;

    private Vector3 direction = Vector3.zero;

    private bool bActive;
    private float timer;

    // Read only
    public bool BActive => bActive;

    // First function to be called when the object is created and called once per session
    private void Awake()
    {
        //firingSound = GetComponent<AudioSource>();
        //collisionSound = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (bActive)
        {
            timer += Time.fixedDeltaTime;

            if (timer >= lifetime)
            {
                DisableSelf();
            }

            transform.position += direction * Time.fixedDeltaTime * speed;
        }
    }

    public void Activate(float spd, Vector3 dir)
    {
        bActive = true;

        // Set the mRenderer and cCollider back to enabled since they've been disabled in DisableSelf
        mRenderer.enabled = true;
        cCollider.enabled = true;
        if (firingSound)
        {
            firingSound.Play();
        }

        // Set the direction and speed of the bullets
        direction = dir;
        speed = spd;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if aSource exists (null check)
        if (collisionSound)
        {
            collisionSound.Play();
        }

        // disappear
        DisableSelf();
    }

    private void DisableSelf()
    {
        mRenderer.enabled = false;
        cCollider.enabled = false;
        bActive = false;
        timer = 0;

        // gameObject.SetActive(false);
    }

}

