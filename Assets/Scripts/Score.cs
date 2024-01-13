using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour, IGetFinalScore
{
    [Header("Quiz to track score data from")]
    [SerializeField] Quiz quiz;

    TextMeshProUGUI scoreText;
    int currentScore = 0;

    public int FinalScoreFraction
    {
        get => GetScoreFraction(currentScore, quiz.TotalQuestions);
    }

    void Start()
    {
        // Get Score Component
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        if (quiz.IsUserAnswerCorrect)
        {
            // Increment the Score
            currentScore++;

            // Change Back for the next Question
            quiz.IsUserAnswerCorrect = false;

            // Cacculate the fraction to output
            int score = GetScoreFraction(currentScore, quiz.TotalQuestions);

            // Output the Text
            scoreText.text = $"Score: {score}%";
        }
    }

    int GetScoreFraction(int correct_question, int total_question)
    {
        return Mathf.RoundToInt((float)correct_question / total_question * 100);
    }
}
