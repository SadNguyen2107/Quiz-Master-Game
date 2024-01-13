using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Quiz To Track")]
    [SerializeField] Quiz quiz;

    // Timer Attributes Objects
    Image timerImage;
    TextMeshProUGUI timerText;

    [Header("Timer Attribute")]
    [SerializeField] float secToCompleteQuestion;
    [SerializeField] float secToShowCorrectedAnswer;
    float currentTime;

    void Start()
    {
        currentTime = secToCompleteQuestion;

        // Get Timer Image and Timer Text
        timerImage = GetComponentInChildren<Image>();
        timerText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        UpdateTimer();
    }

    // Update the Image
    void UpdateTimerImage(UserState userState)
    {
        float fill_amount_fraction;
        // Show time to Answer Question
        if (userState == UserState.AnsweringPhase)
        {
            // Update the fill amount fraction every sec
            fill_amount_fraction = currentTime / secToCompleteQuestion;
            timerImage.fillAmount = fill_amount_fraction;

        }
        else if (userState == UserState.ShowingCorrectAnswer)  // Show time to Show Answer
        {
            // Update the fill amount fraction every sec
            fill_amount_fraction = currentTime / secToShowCorrectedAnswer;
            timerImage.fillAmount = fill_amount_fraction;
        }
    }

    // Update the Text
    void UpdateTimerText()
    {
        // Update the Text Every Sec
        int seconds = Mathf.RoundToInt(currentTime);
        timerText.text = $"{seconds}s";
    }

    void UpdateTimer()
    {
        // Minus 1 sec Every <-> frame
        currentTime -= Time.deltaTime;

        // IF User Finished Answer
        if (quiz.UserState == UserState.FinishedAnswer)
        {
            quiz.UserState = UserState.ShowingCorrectAnswer;
            currentTime = secToShowCorrectedAnswer;
        }
        else if (quiz.UserState == UserState.AnsweringPhase && currentTime < 0)   // Or Timer has Run Out
        {
            quiz.UserState = UserState.ShowingCorrectAnswer;
            currentTime = secToShowCorrectedAnswer;

            // Select A Random Button
            quiz.OnAnswerButtonClick();
        }
        else if (quiz.UserState == UserState.ShowingCorrectAnswer && currentTime < 0)   // End Of Showing
        {
            quiz.UserState = UserState.FinishedShowingAnswer;
            currentTime = secToCompleteQuestion;
        }

        // Update Both the Visual and Text 
        UpdateTimerImage(quiz.UserState);
        UpdateTimerText();
    }
}
