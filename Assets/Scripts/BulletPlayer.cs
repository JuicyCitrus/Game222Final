using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BulletPlayer : MonoBehaviour
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

    [SerializeField]
    private float damage;


    private float speed = 5;
    private Vector3 direction = Vector3.zero;
    private bool bActive;
    private float timer;

    // Read only
    public bool BActive => bActive;

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
        // Play the collision sound if it exists
        if (collisionSound)
        {
            collisionSound.Play();
        }

        // Get the Turret Health Script and use it to make the turret take damage
        if (other.TryGetComponent<TurretHealth>(out TurretHealth health))
        {
            health.UpdateHealth(damage);
        }

        // Disappear after the collision
        DisableSelf();
    }

    private void DisableSelf()
    {
        mRenderer.enabled = false;
        cCollider.enabled = false;
        bActive = false;
        timer = 0;
    }
}
