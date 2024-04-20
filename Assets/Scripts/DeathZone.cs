using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private PlayerHealth PH;
    private float damage;

    private void OnTriggerEnter(Collider other)
    {
        PH = other.GetComponent<PlayerHealth>();
        damage =  PH.hp;
        PH.UpdateHealth(damage);
    }
}
