using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("All the Canvas will be controlled here")]
    [SerializeField] Quiz quizCanvas;
    [SerializeField] EndScreen endScreenCanvas;

    void Start()
    {
        // Set the quizCanvas first -> endScreenCanvas
        quizCanvas.gameObject.SetActive(true);
        endScreenCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (quizCanvas.UserState == UserState.FinishedAllQuestions)
        {
            quizCanvas.gameObject.SetActive(false);
            endScreenCanvas.gameObject.SetActive(true);
        }
    }

    // If User Press play Again
    public void OnReplayLevel()
    {
        // Load the Current Active Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
