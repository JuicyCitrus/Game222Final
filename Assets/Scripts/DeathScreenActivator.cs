using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenActivator : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth PH;

    private void Update()
    {
        // Show death screen when the player dies
        if(PH.hp <= 0)
        {
            Debug.Log("Player is dead");
            this.gameObject.SetActive(true);
        }

    }
}
