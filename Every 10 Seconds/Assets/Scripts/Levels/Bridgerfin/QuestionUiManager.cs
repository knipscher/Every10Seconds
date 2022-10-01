using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private TextMeshProUGUI answerA;
    [SerializeField] private TextMeshProUGUI answerB;
    [SerializeField] private Button buttonA;
    [SerializeField] private Button buttonB;

    [SerializeField] private QuestionAndAnswerAtlas questionAndAnswerAtlas;
    private QuestionAndAnswers currentQuestionAndAnswers;

    [SerializeField] private float questionTime;
    private float elapsedTime;

    [SerializeField] private Slider suspicionSlider;

    private bool hasAnswered = false;

    private IEnumerator Start()
    {
        suspicionSlider.SetValueWithoutNotify(0);
        SetQuestionAndAnswers();
        
        questionTime = Mathf.Clamp(questionTime - GameManager.instance.level, GameManager.instance.minigameLength / 2, GameManager.instance.minigameLength);
        yield return new WaitForSeconds(questionTime);
        if (!hasAnswered)
        {
            TimeOut();
        }
    }

    private void Update()
    {
        if (!hasAnswered)
        {
            elapsedTime += Time.deltaTime;
            suspicionSlider.SetValueWithoutNotify(elapsedTime / questionTime);
        }
    }

    private void SetQuestionAndAnswers()
    {
        currentQuestionAndAnswers = questionAndAnswerAtlas.GetQuestionAndAnswersByLevel(GameManager.instance.level - 1);
        question.text = currentQuestionAndAnswers.question;
        var random = Random.value;
        if (random > 0.5f)
        {
            answerA.text = currentQuestionAndAnswers.correctAnswer;
            answerB.text = currentQuestionAndAnswers.incorrectAnswer;

            buttonA.onClick.AddListener(CorrectAnswer);
            buttonB.onClick.AddListener(IncorrectAnswer);
        } 
        else 
        {
            answerA.text = currentQuestionAndAnswers.incorrectAnswer;
            answerB.text = currentQuestionAndAnswers.correctAnswer;

            buttonA.onClick.AddListener(IncorrectAnswer);
            buttonB.onClick.AddListener(CorrectAnswer);
        }
    }

    private void CorrectAnswer()
    {
        hasAnswered = true;

        buttonA.gameObject.SetActive(false);
        buttonB.gameObject.SetActive(false);

        question.text = currentQuestionAndAnswers.correctResponse;

        GameManager.instance.Score(Mathf.RoundToInt(questionTime - elapsedTime) + 1);
    }

    private void IncorrectAnswer()
    {
        hasAnswered = true;

        buttonA.gameObject.SetActive(false);
        buttonB.gameObject.SetActive(false);

        question.text = currentQuestionAndAnswers.wrongResponse;

        GameManager.instance.LoseGame();
    }

    private void TimeOut()
    {
        buttonA.gameObject.SetActive(false);
        buttonB.gameObject.SetActive(false);

        question.text = currentQuestionAndAnswers.timeoutResponse;

        GameManager.instance.LoseGame();
    }
}
