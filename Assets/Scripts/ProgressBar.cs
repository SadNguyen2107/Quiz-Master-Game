using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Header("Quiz to track score data from")]
    [SerializeField] Quiz quiz;
    Slider progressBarSlider;

    void Start()
    {
        // Get the progressBarSlider
        progressBarSlider = GetComponentInChildren<Slider>();

        // Set the Min, Max of the Slider
        progressBarSlider.minValue = 0;
        progressBarSlider.maxValue = quiz.TotalQuestions;
    }

    void Update()
    {
        UpdateSlider();
    }

    void UpdateSlider()
    {
        // Get the Current Question Index 
        // Note: +1 to -> No.Question
        if (quiz.UserState == UserState.ShowingCorrectAnswer)
        {
            progressBarSlider.value = quiz.CurrentQuestionIndex + 1;
        }
    }
}
