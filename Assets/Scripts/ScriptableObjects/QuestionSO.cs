using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Question", order = 0)]
public class QuestionSO : ScriptableObject
{
    [Header("Question")]
    [TextArea(minLines: 2, maxLines: 6)]
    [SerializeField] string questionContent;

    [Header("Answers")]
    [SerializeField] string[] allPossibleAnswers = new string[4];

    [Header("Correct Answer At Element")]
    [Range(0, 3)]
    [SerializeField] Byte correctAnswerIndex;

    // Some Getter Methods to Retrieve The value
    // Get Question Content
    public string QuestionContent
    {
        get => questionContent;
    }

    // Get The Possible Answers on this QuestionSO 
    public string[] AllPossibleAnswers
    {
        get => allPossibleAnswers;
    }

    // Get The Correct Answer Index
    public byte CorrectAnswerIndex
    {
        get => correctAnswerIndex;
    }
}