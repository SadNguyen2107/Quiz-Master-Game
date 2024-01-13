using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices.WindowsRuntime;

public class Quiz : MonoBehaviour, IUserState, IQuestion
{
    [Header("Question Objects")]
    [SerializeField] QuestionSO[] questions;
    QuestionSO currentQuestion;
    sbyte currentQuestionIndex = -1;
    public bool IsUserAnswerCorrect
    {
        get;
        set;
    }
    public int CurrentQuestionIndex
    {
        get => currentQuestionIndex;
    }

    public int TotalQuestions
    {
        get => questions.Length;
    }

    [Header("Display Question And Answers")]
    [SerializeField] TextMeshProUGUI questionTextBox;
    [SerializeField] Button[] answerButtons;
    TextMeshProUGUI[] answerTextBox;
    public UserState UserState
    {
        get;
        set;
    }

    [Header("Sprite to show Correct Answer and Default Answer")]
    [SerializeField] Sprite correctAnswerButtonSprite;
    [SerializeField] Sprite defaultAnswerButtonSprite;

    void Start()
    {
        // Get Answer Text Box Components in the Text Box- TMPro
        GetAnswerTMProComponents();

        // Choose A Random Question in questions list
        ChooseRandomQuestion();

        // Display to the Screen
        DisplayToScreen();

        // Wait For User to Answer
        UserState = UserState.AnsweringPhase;
    }

    void Update()
    {
        // Wait For the Timer to Run out if Showing
        if (UserState == UserState.ShowingCorrectAnswer)
        {
            return;
        }

        else if (UserState == UserState.FinishedShowingAnswer)
        {
            // FIXME: 
            // Delete the Answered Question 
            // DeleteQuestionAt(currentQuestionIndex);

            // Choose A Random Question in questions list
            ChooseRandomQuestion();
            if (UserState == UserState.FinishedAllQuestions)
            {
                // End at this step
                return;
            }

            // Display to the Screen
            DisplayToScreen();

            // Wait For User to Answer
            UserState = UserState.AnsweringPhase;

            // Reset all the Buttons to the Default State
            ResetAllAnswerButtonsToDefaultState();
        }
    }

    /// <summary>
    ///     Get Component at Start
    /// </summary>
    void GetAnswerTMProComponents()
    {
        answerTextBox = new TextMeshProUGUI[answerButtons.Length];
        // Get all the Children Component of Object Button
        for (int button_index = 0; button_index < answerButtons.Length; button_index++)
        {
            answerTextBox[button_index] = answerButtons[button_index].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    void ChooseRandomQuestion()
    {
        // Choose a Random Question in question
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            currentQuestion = questions[currentQuestionIndex];
            return;
        }

        // User Has Finished All the Question
        UserState = UserState.FinishedAllQuestions;
    }

    void DeleteQuestionAt(int index)
    {
        // Delete it from the Array
        questions[index] = null;
    }

    void ResetAllAnswerButtonsToDefaultState()
    {
        // Reset all the Buttons to the Default State
        foreach (Button button in answerButtons)
        {
            button.interactable = true;
            button.image.sprite = defaultAnswerButtonSprite;
        }
    }

    /// <summary>
    /// Display Section
    /// </summary>
    //--------------------------------------------------------------
    void DisplayToScreen()
    {
        if (currentQuestionIndex < questions.Length)
        {
            // Display To Screen
            DisplayQuestion();
            DisplayAllPossibleAnswers();
        }
    }

    void DisplayQuestion()
    {
        // Get the Question
        string question_content = currentQuestion.QuestionContent;

        // Change the Content of the Question Text Box
        questionTextBox.text = question_content;
    }

    void DisplayAllPossibleAnswers()
    {
        // Get All the Possible Answers
        string[] all_possible_answers = currentQuestion.AllPossibleAnswers;

        // Change All the Content of the Button Answer 
        for (int button_index = 0; button_index < answerButtons.Length; button_index++)
        {
            answerTextBox[button_index].text = all_possible_answers[button_index];
        }
    }

    void DisplayCorrectAnswer(int index = -1)
    {
        byte correct_answer_index = currentQuestion.CorrectAnswerIndex;
        // Check For Button click 
        if (index == correct_answer_index)
        {
            // Change the Content of the Question Text -> Correct!
            questionTextBox.text = "Correct!";

            // User Answer Correct
            IsUserAnswerCorrect = true;
        }
        else
        {
            questionTextBox.text = $"Wrong! The correct answer is\n {currentQuestion.AllPossibleAnswers[correct_answer_index]}";

            // Change the Color of the Wrong Button To red
            if (index >= 0)
            {
                answerButtons[index].image.color = new Color(255, 0, 0, 255);
            }
        }

        // Change the Correct Button Sprite
        answerButtons[correct_answer_index].image.sprite = correctAnswerButtonSprite;

        // Phase to show the Answer
        UserState = UserState.FinishedAnswer;

        // Disable ALl the Buttons
        DisableAllButtonsClick();
    }
    //--------------------------------------------------------------

    // Onclick Method()
    //------------------------------------------------------------------
    public void OnAnswerButtonClick(int index = -1)
    {
        // Show the Real Answer
        DisplayCorrectAnswer(index);
    }

    void DisableAllButtonsClick()
    {
        foreach (Button answer_button in answerButtons)
        {
            answer_button.interactable = false;
        }
    }
    //-------------------------------------------------------------------------------
}
