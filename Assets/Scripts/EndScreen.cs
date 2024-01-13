using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [Header("To track whether if the user finished all the questions")]
    [SerializeField] Quiz quiz;


    [Header("To display the Final Score")]
    [SerializeField] Score finalScore;
    [SerializeField] TextMeshProUGUI congratsText;
    

    void Start()
    {
        if (quiz.UserState == UserState.FinishedAllQuestions)
        {
            int final_score = finalScore.FinalScoreFraction;

            // Display the Text
            congratsText.text = $"Congratulations!\nYou scored {final_score}%";
        }
    }
}
