using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCounter : MonoBehaviour
{
    private void OnEnable()
    {
        TurretHealth.TurretDeath += Add;
    }

    private void OnDisable()
    {
        TurretHealth.TurretDeath -= Add;
    }

    private void Add()
    {
        Score();
    }

    private void Score()
    {

    }
}
