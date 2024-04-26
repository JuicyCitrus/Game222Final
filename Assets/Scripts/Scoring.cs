using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private float secondsPerPoint;

    private bool alive = true;
    private int score = 0;

    private void Start()
    {
        score = 0;
        StartCoroutine(nameof(ScoreUpdate));
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();
    }

    public void StopScoring()
    {
        StopCoroutine(nameof(ScoreUpdate));
    }

    private void OnEnable()
    {
        ResetButton.ResetScene += Start;
    }
    private void OnDisable()
    {
        ResetButton.ResetScene -= Start;
    }

    IEnumerator ScoreUpdate()
    {
        while(alive)
        {
            yield return new WaitForSeconds(secondsPerPoint);
            UpdateScore(1);
        }
    }
}
