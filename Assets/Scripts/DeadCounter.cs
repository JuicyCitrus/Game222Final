using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCounter : MonoBehaviour
{
    [SerializeField]
    private Scoring scoreboard;

    [SerializeField]
    private int totalTurrets = 0;

    [SerializeField]
    private GameObject victoryScreen;

    private int destroyedTurrets = 0;

    private void OnEnable()
    {
        TurretHealth.TurretDeath += Add;
    }

    private void OnDisable()
    {
        TurretHealth.TurretDeath -= Add;
    }

    private void Update()
    {
        if(destroyedTurrets/totalTurrets == 1)
        {
            victoryScreen.SetActive(true);
            scoreboard.StopScoring();
        }
    }

    private void Add()
    {
        destroyedTurrets++;
        Score();
    }

    private void Score()
    {
        scoreboard.UpdateScore(100);
    }
}
