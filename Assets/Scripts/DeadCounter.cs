using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

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

    private int destroyedTurrets;

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
    }

    private void Update()
    {
        if(destroyedTurrets/totalTurrets == 1)
        {
            victoryScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(SelectedButton);
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
