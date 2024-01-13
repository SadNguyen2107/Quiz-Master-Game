interface IQuestion
{
    public bool IsUserAnswerCorrect
    {
        get;
        set;
    }

    public int CurrentQuestionIndex
    {
        get;
    }

    public int TotalQuestions
    {
        get;
    }
}