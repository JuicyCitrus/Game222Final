using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

public class DeadCounter : MonoBehaviour
{
    [SerializeField]
    private Scoring scoreboard;

    [SerializeField]
    private int totalTurrets = 0;

    [SerializeField]
    private GameObject victoryScreen;

    [SerializeField]
    private GameObject SelectedButton;

    [SerializeField]
    private GameObject Player;

    private int destroyedTurrets;

    private bool Won;

    public static event Action Victory = delegate { };

    private void OnEnable()
    {
        TurretHealth.TurretDeath += Add;
        ResetButton.ResetScene += Start;
    }

    private void OnDisable()
    {
        TurretHealth.TurretDeath -= Add;
        ResetButton.ResetScene -= Start;
    }

    private void Start()
    {
        victoryScreen.SetActive(false);
        destroyedTurrets = 0;
        Won = false;
    }

    private void Update()
    {
        if (destroyedTurrets / totalTurrets == 1)
        {
            if (!Won)
            {
                Vic();
            }
        }
    }

    private void Vic()
    {
        Won = true;
        // UI INteractions
        victoryScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SelectedButton);
        scoreboard.StopScoring();

        // Delegate Call
        Victory();
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
