using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private PlayerHealth PH;
    private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth health))
        {
            damage = health.hp;
            health.UpdateHealth(damage);
        }
    }
}
